using IronPython.Hosting;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeImage(Button button)
        {
            throw new NotImplementedException();
        }

        private void ButtonMic1On_Click(object sender, RoutedEventArgs e)
        {
            var py = Python.CreateEngine();

            try
            {
                //MessageBox.Show(py.Execute("print('Hello World')"));
                var p = py.Execute("2+3");
                MessageBox.Show(p.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                throw;
            }
        }
    }
}
