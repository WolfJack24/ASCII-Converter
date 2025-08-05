using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Python.Runtime;

namespace Window
{
    public class Program
    {
        static readonly string TargetFolder = "Converter_(ASCII_to_Whatever)";
        static readonly string FileName = "output.txt";
        static readonly string PythonVersion = "313";

        static string OutputPath;
        static string ConversionPath;
        static readonly string PythonPath = Path.Combine(
            Environment.GetEnvironmentVariable("LOCALAPPDATA"), 
            "Programs", 
            "Python", 
            $"Python{PythonVersion}", 
            $"python{PythonVersion}.dll"
        );

        static Form mainWindow;
        static TextBox conversionBox;
        static ComboBox options;
        static Button convertBtn;
        static Button saveFileBtn;

        [STAThread]
        static void Main()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo dirInfo = new(currentDirectory);

            while (dirInfo != null && dirInfo.Name != TargetFolder)
            {
                dirInfo = dirInfo.Parent;
            }

            string basePath = dirInfo?.FullName;
            OutputPath = Path.Combine(basePath, FileName);
            ConversionPath = Path.Combine(basePath, "conversion\\");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Runtime.PythonDLL = PythonPath;
            PythonEngine.Initialize();

            mainWindow = new Form
            {
                Text = "ASCII Converter"
            };

            conversionBox = new TextBox
            {
                Size = new Size(121, 21),
                Location = new Point(80, 25)
            };

            options = new ComboBox
            {
                Size = new Size(121, 21),
                Location = new Point(80, 50)
            };

            convertBtn = new Button
            {
                Text = "Convert!",
                Size = new Size(121, 21),
                Location = new Point(80, 75)
            };

            saveFileBtn = new Button
            {
                Text = "Save?",
                Size = new Size(121, 21),
                Location = new Point(80, 96)
            };

            options.Items.Add("Binary");
            options.Items.Add("Decimal");
            options.Items.Add("Hexadecimal");

            convertBtn.Click += new EventHandler(ConvertBtn_Clicked);
            saveFileBtn.Click += new EventHandler(SaveFileBtn_Clicked);

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
                var PyResult = script.InvokeMethod("give_value", [PyText, PyOpt]);
                result = PyResult.ToString();
            }

            string conversion = "Text: " + text + "\n" +
                                "Conversion: " + option + "\n" +
                                "Result: " + result + "\n";

            return conversion;
        }

        private static void ConvertBtn_Clicked(object sender, EventArgs e)
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

                    DialogResult BoxResult = MessageBox.Show(
                                                            converted_text + "\n" + "Do you want to save?", 
                                                            "Converted!", 
                                                            MessageBoxButtons.YesNo
                                                        );

                    if (BoxResult == DialogResult.Yes)
                    {
                        using StreamWriter writer = File.AppendText(OutputPath);
                        writer.WriteLine(
                                        "Dialog Save" + "\n" + 
                                        "--------------------" + "\n" +
                                        converted_text
                                    );
                    }
                }
            }
        }

        private static void SaveFileBtn_Clicked(object sender, EventArgs e)
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

                    using StreamWriter writer = File.AppendText(OutputPath);
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