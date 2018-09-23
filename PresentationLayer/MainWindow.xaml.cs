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
            ButtonStop.Visibility = Visibility.Hidden;
            mciSendString("open new Type waveaudio Alias recsound", null, 0, IntPtr.Zero);
        }

        public void ChangeImage(Button button)
        {
            throw new NotImplementedException();
        }

        private void ButtonMic1On_Click(object sender, RoutedEventArgs e)
        {
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
            ButtonStop.Visibility = Visibility.Visible;
        }

        private void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            mciSendString("save recsound d:\\Games\\mic.wav", null, 0, IntPtr.Zero);
            mciSendString("close recSound", null, 0, IntPtr.Zero);
            ButtonStop.Visibility = Visibility.Hidden;
        }
    }
}
