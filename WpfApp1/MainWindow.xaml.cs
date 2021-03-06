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


        private void StartColcButton(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
             {
                 
                
                 // когда мф встречаем ошибку что не можем получить данные из другого потока то там нужно наши данные которые изменяются разбить на два этапа

                 var resulr = LongPorcesCalc();//занести их в переменную и получить их через метод Invoke или BeginInvoke

                 Dispatcher.Invoke(() => Title = DateTime.Now.ToString());//тут мы изменияем наше имя окна так чтобы приложение не зависло

                 ResultTextBlock.Dispatcher.Invoke(() =>//при помощи метода инвоке мы заставляем наши потоки ждать пока не завершиться оснвной 
                 {
                     ResultTextBlock.Text = resulr;
                 });
             });// таким рьразом мы сделали наш поток фоновым и теперь и пробработки данного метода приложения не будет зависать 
            thread.Start();
           
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

        private void StartCalcFibonachi(object sender, RoutedEventArgs e)
        {
            var againThread = new Thread(() =>
             {
                 Calc();
                 var fib = Fibonachssi();
                 Fibonachi.Dispatcher.Invoke(() =>
                 {
                     Fibonachi.Text = fib;
                 });
             });
            againThread.Start();
        }

        private static readonly object SyncRoot = new object();
        private static readonly object SyncRoot1 = new object();
        private void Calc()
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
            }
        }
         

        private string Fibonachssi()
        {
            lock(SyncRoot1)
            {
                int randNum = new Random().Next(1, 100);
                if (randNum == 1)
                {
                    return Convert.ToString(1);
                }
                Thread.Sleep(250);
                var num = Convert.ToString(randNum * (randNum - 1));
                return num;
            }
            
        }

    }
}
