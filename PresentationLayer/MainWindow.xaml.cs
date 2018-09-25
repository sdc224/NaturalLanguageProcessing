using NAudio.Wave;
using System;
using System.Globalization;
using System.IO;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*[DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder retString, int returnLength,
            IntPtr callBack);*/

        private static bool _completed;

        private readonly BufferedWaveProvider _bwp;
        private WaveIn _waveIn;
        private readonly WaveOut _waveOut;
        private WaveFileWriter _writer;
        private WaveFileReader _reader;
        private const string Output = @"D:\Games\audio.wav";

        public MainWindow()
        {
            InitializeComponent();
            _waveOut = new WaveOut();
            _waveIn = new WaveIn();

            _waveIn.DataAvailable += WaveIn_DataAvailable;
            _waveIn.WaveFormat = new WaveFormat(16000, 1);

            _bwp = new BufferedWaveProvider(_waveIn.WaveFormat) { DiscardOnBufferOverflow = true };

            //mciSendString("open new Type waveaudio Alias recsound", null, 0, IntPtr.Zero);
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
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

            //mciSendString("record recsound", null, 0, IntPtr.Zero);

            if (WaveIn.DeviceCount < 1)
                throw new InvalidOperationException("No microphone");

            _waveIn.StartRecording();

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
            /*mciSendString("save recsound d:\\Games\\mic.wav", null, 0, IntPtr.Zero);
            mciSendString("close recSound", null, 0, IntPtr.Zero);*/
            if (!(sender is Button button)) return;

            _waveIn.StopRecording();

            if (File.Exists(@"D:\Games\audio.wav"))
                File.Delete(@"D:\Games\audio.wav");

            _writer = new WaveFileWriter(Output, _waveIn.WaveFormat);

            switch (button.Name)
            {
                case "ButtonMic1Off":
                    ButtonMic1On.IsEnabled = true;
                    ButtonMic1Off.IsEnabled = false;
                    ButtonMic1Convert.IsEnabled = true;
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

            var buffer = new byte[_bwp.BufferLength];
            const int offset = 0;
            var count = _bwp.BufferLength;

            var read = _bwp.Read(buffer, offset, count);

            if (count > 0)
            {
                _writer.Write(buffer, offset, read);
            }

            _waveIn.Dispose();
            _waveIn = null;
            _writer.Close();
            _writer = null;

            _reader = new WaveFileReader(@"D:\Games\audio.wav");
            _waveOut.Init(_reader);
            _waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
            _waveOut.Play();
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            _waveOut.Stop();
            _reader.Close();
            _reader = null;
        }

        private void ButtonMicConvert_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonMic1Convert.IsEnabled = false;
            if (File.Exists(@"D:\Games\audio.wav"))
            {
                using (var recognizer = new SpeechRecognitionEngine(new CultureInfo("en-GB")))
                {
                    // Create and Load Grammar
                    var dictation = new DictationGrammar();
                    recognizer.LoadGrammar(dictation);

                    // Configure the input
                    recognizer.SetInputToWaveFile(@"D:\Games\audio.wav");
                    recognizer.SpeechRecognized += RecognizerOnSpeechRecognized;
                    recognizer.RecognizeCompleted += Recognizer_RecognizeCompleted;

                    _completed = false;
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);

                    while (!_completed)
                        MessageBox.Show("Working...Wait");

                    MessageBox.Show("Done :)");
                }
            }

            else
            {
                throw new InvalidOperationException("Audio file missing");
            }
        }

        private static void RecognizerOnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var fileName = @"D:\text.txt";

            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file 
                using (var sw = File.CreateText(fileName))
                {
                    if (e.Result != null)
                    {
                        sw.WriteAsync(e.Result.Text + " ");
                    }
                    else
                    {
                        sw.Write("Recognized text not available...");
                    }
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Error encountered, {e.Error.GetType().Name}: {e.Error.Message}");
            }
            if (e.Cancelled)
            {
                MessageBox.Show("Operation cancelled.");
            }
            if (e.InputStreamEnded)
            {
                MessageBox.Show("End of stream encountered.");
            }
            _completed = true;
        }
    }
}
