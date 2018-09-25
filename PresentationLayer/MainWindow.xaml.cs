using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder retString, int returnLength,
            IntPtr callBack);

        public MainWindow()
        {
            InitializeComponent();
            mciSendString("open new Type waveaudio Alias recsound", null, 0, IntPtr.Zero);
        }

        private void ButtonStartOn_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            /*var py = Python.CreateEngine();

            try
            {
                //MessageBox.Show(py.Execute("print('Hello World')"));
                var p = py.Execute("2+3");
                MessageBox.Show(p.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }*/

            mciSendString("record recsound", null, 0, IntPtr.Zero);

            switch (button.Name)
            {
                case "ButtonMic1On":
                    ButtonMic1Off.IsEnabled = true;
                    ButtonMic1On.IsEnabled = false;
                    break;

                case "ButtonMic2On":
                    ButtonMic2Off.IsEnabled = true;
                    ButtonMic2On.IsEnabled = false;
                    break;

                case "ButtonMic3On":
                    ButtonMic3Off.IsEnabled = true;
                    ButtonMic3On.IsEnabled = false;
                    break;

                case "ButtonMic4On":
                    ButtonMic4Off.IsEnabled = true;
                    ButtonMic4On.IsEnabled = false;
                    break;

                case "ButtonMic5On":
                    ButtonMic5Off.IsEnabled = true;
                    ButtonMic5On.IsEnabled = false;
                    break;

                case "ButtonMic6On":
                    ButtonMic6Off.IsEnabled = true;
                    ButtonMic6On.IsEnabled = false;
                    break;

                case "ButtonMic7On":
                    ButtonMic7Off.IsEnabled = true;
                    ButtonMic7On.IsEnabled = false;
                    break;

                case "ButtonMic8On":
                    ButtonMic8Off.IsEnabled = true;
                    ButtonMic8On.IsEnabled = false;
                    break;

                default:
                    throw new InvalidOperationException("Invalid Button selected");
            }
        }

        private void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            mciSendString("save recsound d:\\Games\\mic.wav", null, 0, IntPtr.Zero);
            mciSendString("close recSound", null, 0, IntPtr.Zero);
            if (!(sender is Button button)) return;
            switch (button.Name)
            {
                case "ButtonMic1Off":
                    ButtonMic1On.IsEnabled = true;
                    ButtonMic1Off.IsEnabled = false;
                    break;

                case "ButtonMic2Off":
                    ButtonMic2On.IsEnabled = true;
                    ButtonMic2Off.IsEnabled = false;
                    break;

                case "ButtonMic3Off":
                    ButtonMic3On.IsEnabled = true;
                    ButtonMic3Off.IsEnabled = false;
                    break;

                case "ButtonMic4Off":
                    ButtonMic4On.IsEnabled = true;
                    ButtonMic4Off.IsEnabled = false;
                    break;

                case "ButtonMic5Off":
                    ButtonMic5On.IsEnabled = true;
                    ButtonMic5Off.IsEnabled = false;
                    break;

                case "ButtonMic6Off":
                    ButtonMic6On.IsEnabled = true;
                    ButtonMic6Off.IsEnabled = false;
                    break;

                case "ButtonMic7Off":
                    ButtonMic7On.IsEnabled = true;
                    ButtonMic7Off.IsEnabled = false;
                    break;

                case "ButtonMic8Off":
                    ButtonMic8On.IsEnabled = true;
                    ButtonMic8Off.IsEnabled = false;
                    break;

                default:
                    throw new InvalidOperationException("Invalid Button selected");
            }
        }

        private void ButtonMicConvert_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
