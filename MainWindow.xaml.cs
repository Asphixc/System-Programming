using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace Family
{
    public partial class MainWindow : Window
    {
        private Process childProcess;

        public MainWindow()
        {
            InitializeComponent();

            ParentProcessIdTextBlock.Text = Process.GetCurrentProcess().Id.ToString();
        }

        private void StartChildButton_Click(object sender, RoutedEventArgs e)
        {
            if (childProcess != null && !childProcess.HasExited)
            {
                MessageBox.Show("Дочерний процесс уже запущен.");
                return;
            }

            try
            {
                string fileName = Process.GetCurrentProcess().MainModule.FileName;

                childProcess = new Process();
                childProcess.StartInfo.FileName = fileName;
                childProcess.StartInfo.Arguments = "child";
                childProcess.EnableRaisingEvents = true;
                childProcess.Exited += ChildProcess_Exited;

                childProcess.Start();

                ChildProcessIdTextBlock.Text = childProcess.Id.ToString();
                StatusTextBlock.Text = "Дочерний процесс запущен.";

                StartChildButton.IsEnabled = false;
                CloseChildButton.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Ошибка при запуске дочернего процесса.");
            }
        }

        private void CloseChildButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (childProcess != null && !childProcess.HasExited)
                {
                    childProcess.Kill();

                    ChildProcessIdTextBlock.Text = "-";
                    StatusTextBlock.Text = "Дочерний процесс завершен.";

                    StartChildButton.IsEnabled = true;
                    CloseChildButton.IsEnabled = false;

                    childProcess.Dispose();
                    childProcess = null;
                }
                else
                {
                    MessageBox.Show("Дочерний процесс не запущен.");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при завершении дочернего процесса.");
            }
        }

        private void ChildProcess_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                ChildProcessIdTextBlock.Text = "-";
                StatusTextBlock.Text = "Дочерний процесс завершен.";

                StartChildButton.IsEnabled = true;
                CloseChildButton.IsEnabled = false;

                if (childProcess != null)
                {
                    childProcess.Dispose();
                    childProcess = null;
                }
            });
        }
    }
}
