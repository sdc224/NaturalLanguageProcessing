using System.IO;
using System.Windows;

namespace LanguageProcessor
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Window
    {
        private static readonly string Files = Directory.GetCurrentDirectory();

        public ResultPage(string buttonName)
        {
            InitializeComponent();
            /*using (var file = new FileStream(Path.Combine(Files, buttonName), FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        RichTextBox.AppendText(reader.ReadLine());
                    }
                }
            }*/
        }

        /*protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }*/
    }
}
