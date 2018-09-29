﻿using NAudio.Wave;
using PresentationLayer;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;

namespace LanguageProcessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveIn _waveIn;
        private WaveOut _waveOut;
        private WaveFileWriter _writer;
        private WaveFileReader _reader;
        private static readonly string Files = Directory.GetCurrentDirectory();
        private static readonly string AudioFolder = Path.Combine(Files, "Audio");
        private static readonly string PythonFolder = Path.Combine(Files, "Python");
        private static readonly string AudioFile = Path.Combine(AudioFolder, "audio.wav");
        private static readonly string PythonExe = Path.Combine(PythonFolder, "audiototxt.exe");
        private static string _result;

        public MainWindow()
        {
            InitializeComponent();
            _waveOut = new WaveOut();
            _waveIn = new WaveIn();

            _waveIn.DataAvailable += WaveIn_DataAvailable;
            _waveIn.WaveFormat = new WaveFormat(16000, 1);

            Console.WriteLine(Files);
            var zipPath = Files + @"\Python.zip";

            if (!Directory.Exists(PythonFolder))
                ZipFile.ExtractToDirectory(zipPath, PythonFolder);

            if (!Directory.Exists(AudioFolder))
                Directory.CreateDirectory(AudioFolder);
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_writer == null)
                return;

            _writer.Write(e.Buffer, 0, e.BytesRecorded);
            _writer.Flush();

        }

        private void ButtonStartOn_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;

            _waveOut = new WaveOut();
            _waveIn = new WaveIn { WaveFormat = new WaveFormat(16000, 1) };

            _waveIn.DataAvailable += WaveIn_DataAvailable;

            if (WaveIn.DeviceCount < 1)
                throw new InvalidOperationException("No microphone");

            if (File.Exists(AudioFile))
                File.Delete(AudioFile);

            _writer = new WaveFileWriter(AudioFile, _waveIn.WaveFormat);

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
            if (!(sender is Button button)) return;

            _waveIn.StopRecording();


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

            _waveIn.Dispose();
            _waveIn = null;
            _writer.Close();
            _writer = null;

            _reader = new WaveFileReader(AudioFile);
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

            var window = new ResultPage();

            if (File.Exists(AudioFile))
            {
                try
                {
                    RunCmd(PythonExe, AudioFile);
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                MessageBox.Show("Done :)");

                window.RichTextBox.AppendText(_result);
                window.Owner = this;
                window.ShowDialog();
                //Hide();
                ButtonMic1Convert.IsEnabled = false;
            }

            else
            {
                throw new InvalidOperationException("Audio file missing");
            }
        }

        private static void RunCmd(string fileName, string argument)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (argument == null) throw new ArgumentNullException(nameof(argument));

            var start = new ProcessStartInfo
            {
                FileName = "\"" + fileName + "\"",     //cmd is full path to python.exe
                Arguments = "\"" + argument + "\"",   //args is path to .py file and any cmd line args
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UserName = string.Empty,
                Password = null
            };

            using (var process = Process.Start(start))
            {
                if (process == null) return;

                using (var reader = process.StandardOutput)
                {
                    _result = reader.ReadToEnd();

                    /*var fileName = $@"{UserPath}\AppData\Local\Temp\text.txt";

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
                            sw.Write(result + " ");
                        }

                        //_completed = true;
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }*/
                }
            }
        }

        /*private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            var dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (!copySubDirs) return;
            foreach (var subDir in dirs)
            {
                var tempPath = Path.Combine(destDirName, subDir.Name);
                DirectoryCopy(subDir.FullName, tempPath, true);
            }
        }

        private static string LocateExe(string filename)
        {
            var path = Environment.GetEnvironmentVariable("path");
            if (path == null) return string.Empty;
            var folders = path.Split(';');
            foreach (var folder in folders)
            {
                if (File.Exists(folder + filename))
                {
                    return folder + filename;
                }

                if (File.Exists(folder + "\\" + filename))
                {
                    return folder + "\\" + filename;
                }
            }

            return string.Empty;
        }*/

        /*private static void RecognizerOnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            const string fileName = @"D:\text.txt";

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
                MessageBox.Show("End of file adding.");
            }
            _completed = true;
        }*/
    }
}
