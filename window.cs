using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Python.Runtime;

namespace Window
{
    public class Program
    {
        static readonly string FileName = "output.txt";

        static readonly string OutputPath = Path.Combine(Environment.CurrentDirectory, FileName);
        static readonly string ConversionPath = Path.Combine(Environment.CurrentDirectory, "conversion\\");
        static readonly string PythonPath = Convert.ToString(Environment.GetEnvironmentVariable("PYTHON_PATH"));

        static Form mainWindow;
        static TextBox conversionBox;
        static ComboBox options;
        static Button convertBtn;
        static Button saveFileBtn;

        [STAThread]
        static void Main()
        {
            var dotenv = Path.Combine(Environment.CurrentDirectory, ".env");
            DotEnv.Load(dotenv);

            RunApp();
        }

        static void RunApp()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Runtime.PythonDLL = PythonPath;
            PythonEngine.Initialize();

            mainWindow = new Form();
            conversionBox = new TextBox();
            options = new ComboBox();
            convertBtn = new Button();
            saveFileBtn = new Button();

            mainWindow.Text = "ASCII Converter";

            conversionBox.Size = new Size(121, 21);
            conversionBox.Location = new Point(80, 25);

            options.Size = new Size(121, 21);
            options.Location = new Point(80, 50);

            convertBtn.Text = "Convert!";
            convertBtn.Size = new Size(121, 21);
            convertBtn.Location = new Point(80, 75);

            saveFileBtn.Text = "Save?";
            saveFileBtn.Size = new Size(121, 21);
            saveFileBtn.Location = new Point(80, 96);

            options.Items.Add("Binary");
            options.Items.Add("Decimal");
            options.Items.Add("Hexadecimal");

            convertBtn.Click += new EventHandler(_convertBtn_Clicked);
            saveFileBtn.Click += new EventHandler(_saveFileBtn_Clicked);

            mainWindow.Controls.Add(conversionBox);
            mainWindow.Controls.Add(options);
            mainWindow.Controls.Add(convertBtn);
            mainWindow.Controls.Add(saveFileBtn);

            Application.Run(mainWindow);
        }

        private static string ConvertString(string text, string option)
        {
            string result = "";
                    
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(ConversionPath);

                var script = Py.Import("convert");
                var PyText = new PyString(text);
                var PyOpt = new PyString(option);
                var PyResult = script.InvokeMethod("give_value", new PyObject[] {PyText, PyOpt});
                result = PyResult.ToString();
            }

            string conversion = "Text: " + text + "\n" +
                                "Conversion: " + option + "\n" +
                                "Result: " + result + "\n";

            return conversion;
        }

        private static void _convertBtn_Clicked(object sender, EventArgs e)
        {
            string text = conversionBox.Text;
            string opt = options.GetItemText(options.SelectedItem);
            
            if (conversionBox.Text == string.Empty)
            {
                MessageBox.Show("Empty Text Box");
            }
            else
            {

                if (opt == string.Empty)
                {
                    MessageBox.Show("No Combo Box Selection");
                }
                else
                {
                    string converted_text = ConvertString(text, opt);

                    DialogResult BoxResult = MessageBox.Show(converted_text + "\n" + "Do you want to save?", "Converted!",MessageBoxButtons.YesNo);

                    if (BoxResult == DialogResult.Yes)
                    {
                        using (StreamWriter writer = File.AppendText(OutputPath))
                        {
                            writer.WriteLine(
                                            "Dialog Save" + "\n" + 
                                            "--------------------" + "\n" +
                                            converted_text
                                        );
                        }
                    }
                }
            }
        }

        private static void _saveFileBtn_Clicked(object sender, EventArgs e)
        {
            string text = conversionBox.Text;
            string opt = options.GetItemText(options.SelectedItem);
            
            if (conversionBox.Text == string.Empty)
            {
                MessageBox.Show("Empty Text Box");
            }
            else
            {
                if (opt == string.Empty)
                {
                    MessageBox.Show("No Combo Box Selection");
                }
                else
                {
                    string converted_text = ConvertString(text, opt);

                    using (StreamWriter writer = File.AppendText(OutputPath))
                    {
                        writer.WriteLine(
                                        "Save without Dialog" + "\n" + 
                                        "--------------------" + "\n" +
                                        converted_text
                                    );
                    }
                }
            }
        }
    }
}