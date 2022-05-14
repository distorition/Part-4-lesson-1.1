using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void StartColcButton(object sender, RoutedEventArgs e)
        {
            //var thread = new Thread(() =>
            // {


            //     // когда мф встречаем ошибку что не можем получить данные из другого потока то там нужно наши данные которые изменяются разбить на два этапа

            //     var resulr = LongPorcesCalc();//занести их в переменную и получить их через метод Invoke или BeginInvoke

            //     Dispatcher.Invoke(() => Title = DateTime.Now.ToString());//тут мы изменияем наше имя окна так чтобы приложение не зависло

            //     ResultTextBlock.Dispatcher.Invoke(() =>//при помощи метода инвоке мы заставляем наши потоки ждать пока не завершиться оснвной 
            //     {
            //         ResultTextBlock.Text = resulr;
            //     });
            // });// таким рьразом мы сделали наш поток фоновым и теперь и пробработки данного метода приложения не будет зависать 
            //thread.Start();
            var resukt = await Task.Run(() => LongPorcesCalc());
            ResultTextBlock.Text = resukt.ToString();
           
        }
        private CancellationTokenSource cancellation;// тут мы выставили флаг на наш токен
        private async void StartProgres(object sender, RoutedEventArgs e) //все что написанно в методе StartColcButton мы уместили сюда в две строки 
        {
            
            //sender это и есть наша кнопка старта 
            //if(sender is not Button StartButton)// такаяпроверка доступна только в С 9.0
            //{
            //    return;
            //}
            StartButton.IsEnabled = false; //тут переключаем наши кнопки
            CancelButton.IsEnabled = true;

            //прежде чем получатть прогресс нам надо его обьявить 
            IProgress<double> progress = new Progress<double>(p => ProgressInfo.Value = p * 100);// где бы его не вызвали , он всегда будет запускаться в потоке интерфейса

            var cancelToken = new CancellationTokenSource();// то что генерирует токены отмены , через нее мы можем отменять операции 
            cancellation = cancelToken;
            // cancelToken.Cancel();/// у всех токенов отмены произойдет отмена операции 

            try// если мы собираемся что то отменять при помщи токенов , то нам нужно все это засунуть в конструкцию try cath
            {
                var result = await Task.Run(() => LongPorcesCalcAsync(20, progress, cancelToken.Token));//запуск асинхронно и паралельно 
                ResultText.Text = result.ToString();
            }
            catch (OperationCanceledException)// таким образом наша программа не ляжет полностью при отмены операции 
            {
                progress.Report(0);
                ResultText.Text = "Операция отменена";
            }

           // ResultText.Text = await LongPorcesCalcAsync(); //таким образом мы имея асинхронный метод можем еще больше сократить наш код
            StartButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
           //ResultText.Text = await Task.Run(() => LongPorcesCalc());// вобще в одну строчку сделали 
        }
        private  void ClalcButton(object sender, RoutedEventArgs e)
        {
            cancellation.Cancel();// а тут мы при помщи токена отменяем нашу операцию
        }
        private string LongPorcesCalc(int time = 50)
        {
            if (time > 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(time);
                }
            }
            return DateTime.Now.ToString();
        }
                                                                    //рекомендуется добвлять эти параметры (Progress,token)во все асинхронные методы последними параметрами с дефолтными значениями 
        private async Task<string> LongPorcesCalcAsync(int time = 50,IProgress<double> Progress=null, CancellationToken token=default)//Progress для отображения прогресса , token для прирывания 
        {
            token.ThrowIfCancellationRequested();// в асинхронных метода проверка на отмену происходит в трех места ,  в начале , в цике , и перед завершением потому что в любой момент операция может быть отменена 
            const int iterationCount = 100;
            if (time > 0)
            {
                for (int i = 0; i < iterationCount; i++)
                {
                    if (token.IsCancellationRequested)//тут мы проверяем была ли произведена отмена операции 
                    {
                        token.ThrowIfCancellationRequested();//отменя происходит путием генирации ошибки
                    }
                    await Task.Delay(time).ConfigureAwait(false);// при помощи Delay наш асинхронный  код засыпает , CongigureAwait чтобы запустили в любом другом потоке 
                    //Thread.Sleep(time);

                    Progress.Report((double)i / iterationCount);
                }
                Progress.Report(1);// чтобы у нас дошло до конца мы просто в самом конце сделаем его максимальным значением 
            }
            token.ThrowIfCancellationRequested();
            return DateTime.Now.ToString();
        }

        private async void StartCalcFibonachi(object sender, RoutedEventArgs e)
        {
            ///все что в коментах то было запущено через потоки 
            //var againThread = new Thread(() =>
            // {
            //     Calc();
            //     var fib = Fibonachssi();
            //     Fibonachi.Dispatcher.Invoke(() =>
            //     {
            //         Fibonachi.Text = fib;
            //     });
            // });
            //againThread.Start();
            var clas = await Task.Run(() => Calc());
            var result = await Task.Run(() => Fibonachssi());
            Fibonachi.Text = result.ToString();
        }

        private static readonly object SyncRoot = new object();
        private static readonly object SyncRoot1 = new object();
        private int Calc()
        {
            lock (SyncRoot)
            {
                var num = Enumerable.Range(1, 3).Select(x => x);
                foreach (var item in num)
                {
                    Taimer.Dispatcher.Invoke(() =>
                    {
                        Taimer.Text = Convert.ToString(item);
                    });
                    Thread.Sleep(250);
                }
                return 1;
            }
        }
         

        private string Fibonachssi()
        {
            lock(SyncRoot1)
            {
                int randNum = new Random().Next(1, 100);
                if (randNum == 1)
                {
                    return "1".ToString();
                }
                Thread.Sleep(250);
                var num = (randNum * (randNum - 1)).ToString();
                return num;
            }
            
        }
    }
}
