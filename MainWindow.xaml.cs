using System;
using System.Threading;
using System.Windows;

namespace Lesson5.Desktop
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            long firstNumber;
            long secondNumber;

            try
            {
                firstNumber = long.Parse(FirstNumber.Text);
                secondNumber = long.Parse(SecondNumber.Text);
            }
            catch
            {
                MessageBox.Show("Введите два целых числа");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            var cancellationToken = _cancellationTokenSource.Token;

            ChangeStatusControlElements();

            ThreadPool.QueueUserWorkItem(_ => CalculateSum(firstNumber, secondNumber, cancellationToken));
        }

        private void DifferenceButton_Click(object sender, RoutedEventArgs e)
        {
            long firstNumber;
            long secondNumber;

            try
            {
                firstNumber = long.Parse(FirstNumber.Text);
                secondNumber = long.Parse(SecondNumber.Text);
            }
            catch
            {
                MessageBox.Show("Введите два целых числа");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            var cancellationToken = _cancellationTokenSource.Token;

            ChangeStatusControlElements();

            ThreadPool.QueueUserWorkItem(_ => CalculateDifference(firstNumber, secondNumber, cancellationToken));
        }

        private void CalculateSum(long firstNumber, long secondNumber, CancellationToken cancellationToken)
        {
            try
            {
                long sum = firstNumber + secondNumber;

                cancellationToken.ThrowIfCancellationRequested();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Result.Text = sum.ToString();
                });
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Операция успешно отменена");
            }
            finally
            {
                Application.Current.Dispatcher.Invoke(ChangeStatusControlElements);
            }

            SomeAction(new SomeClass1());

            SomeAction(new SomeClass2());
        }

        private void CalculateDifference(long firstNumber, long secondNumber, CancellationToken cancellationToken)
        {
            try
            {
                long difference = firstNumber - secondNumber;

                cancellationToken.ThrowIfCancellationRequested();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Result.Text = difference.ToString();
                });
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Операция успешно отменена");
            }
            finally
            {
                Application.Current.Dispatcher.Invoke(ChangeStatusControlElements);
            }

            SomeAction(new SomeClass1());

            SomeAction(new SomeClass2());
        }

        private void SomeAction(AbstractSomeClass abstractSomeClass)
        {
            abstractSomeClass.Foo();
        }

        abstract class AbstractSomeClass
        {
            public abstract void Foo();
        }

        class SomeClass1 : AbstractSomeClass
        {
            public override void Foo()
            {
            }
        }

        class SomeClass2 : AbstractSomeClass
        {
            public override void Foo()
            {
            }
        }

        private void ChangeStatusControlElements()
        {
            FirstNumber.IsEnabled = !FirstNumber.IsEnabled;

            SecondNumber.IsEnabled = !SecondNumber.IsEnabled;

            SumButton.IsEnabled = !SumButton.IsEnabled;

            DifferenceButton.IsEnabled = !DifferenceButton.IsEnabled;

            CancellationSumButton.IsEnabled = !CancellationSumButton.IsEnabled;
        }

        private void CancellationSumButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}