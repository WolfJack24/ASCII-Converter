using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Python.Runtime;


namespace Window
{
    public class Program
    {
        static string output_path = @"G:\VSC\Python\Converter_(ASCII_to_Whatever)\output.txt";
        static string conversion_path = @"G:\VSC\Python\Converter_(ASCII_to_Whatever)\conversion\";
        static string python_path = @"C:\Users\Denzil Schroder\AppData\Local\Programs\Python\Python312\python312.dll";

        static Form MainWindow;
        static TextBox ConversionBox;
        static ComboBox Options;
        static Button ConvertBtn;
        static Button SaveFileBtn;

        [STAThread]
        static void Main()
        {
            RunApp();
        }

        static void RunApp()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Runtime.PythonDLL = python_path;
            PythonEngine.Initialize();

            MainWindow = new Form();
            ConversionBox = new TextBox();
            Options = new ComboBox();
            ConvertBtn = new Button();
            SaveFileBtn = new Button();

            ConversionBox.Size = new Size(121, 21);
            ConversionBox.Location = new Point(80, 25);

            Options.Size = new Size(121, 21);
            Options.Location = new Point(80, 50);

            ConvertBtn.Text = "Convert!";
            ConvertBtn.Size = new Size(121, 21);
            ConvertBtn.Location = new Point(80, 75);

            SaveFileBtn.Text = "Save?";
            SaveFileBtn.Size = new Size(121, 21);
            SaveFileBtn.Location = new Point(80, 96);

            Options.Items.Add("Binary");
            Options.Items.Add("Decimal");
            Options.Items.Add("Hexadecimal");

            ConvertBtn.Click += new EventHandler(btn_Clicked);
            SaveFileBtn.Click += new EventHandler(SaveBtn_Clicked);

            MainWindow.Controls.Add(ConversionBox);
            MainWindow.Controls.Add(Options);
            MainWindow.Controls.Add(ConvertBtn);
            MainWindow.Controls.Add(SaveFileBtn);

            Application.Run(MainWindow);
        }

        private static string Convert(string text, string option)
        {
            string result = "";
                    
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(conversion_path);

                var script = Py.Import("convert");
                var PyText = new PyString(text);
                var PyOpt = new PyString(option);
                var PyResult = script.InvokeMethod("give_value", new PyObject[] {PyText, PyOpt});
                result = PyResult.ToString();
            }

            string output = "Text: " + text + "\n" +
                            "Conversion: " + option + "\n" +
                            "Result: " + result + "\n";

            return output;
        }

        private static void btn_Clicked(object sender, EventArgs e)
        {
            string text = ConversionBox.Text;
            string opt = Options.GetItemText(Options.SelectedItem);
            
            if (ConversionBox.Text == string.Empty)
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
                    string converted_text = Convert(text, opt);

                    DialogResult BoxResult = MessageBox.Show(converted_text + "\n" + "Do you want to save?", "Converted!",MessageBoxButtons.YesNo);

                    if (BoxResult == DialogResult.Yes)
                    {
                        using (StreamWriter writer = File.AppendText(output_path))
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

        private static void SaveBtn_Clicked(object sender, EventArgs e)
        {
            string text = ConversionBox.Text;
            string opt = Options.GetItemText(Options.SelectedItem);
            
            if (ConversionBox.Text == string.Empty)
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
                    string converted_text = Convert(text, opt);

                    using (StreamWriter writer = File.AppendText(output_path))
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