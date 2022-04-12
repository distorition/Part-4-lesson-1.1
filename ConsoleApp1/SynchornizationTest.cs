using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class SynchornizationTest
    {
        private static void MonitorTest()
        {
            var sync_rot = new object();
            lock (sync_rot)//любой поток который захочет получить данный обьект они остановятся и станут в очередь 
            {
                Console.WriteLine("Критическая секция ");// как только мы выйдем из этого лока , другие потоки смогут сюда зайти 
            }

            Monitor.Enter(sync_rot);// это то во что разворачивается слово lock
            try
            {
                Console.WriteLine("Критическая секция ");
            }
            finally
            {
                Monitor.Exit(sync_rot);//в том потоке где мы заблокировали мы должы и разблокировать в любом другом случае будет оишбка
            }
        }

        private static void MuteSemaphoretest()
        {
            var mutex = new Mutex(true, "название приложения", out var is_created);//по навазнию можно найти наш мьютекс в жругом приложении а в переменной передается значения первыми ли мы создали его или нет 

            var semaphore = new Semaphore(0, 8);//таким образом мы указываем кол-во входов то есть в наш семафор сможет зайти только 8 потоков
            semaphore.WaitOne();// как только в наш семафор зашло 8 потоков ты мы ожидаем на этом месте 
            if (!semaphore.WaitOne(3000))// так же мы можем блокировать спустя время то есть если у нас не удается подключиться к семафору за 3 сек то мы прекращаем попытку 
            {
                return;
            }
            Console.WriteLine("Критическая секция ");
            semaphore.Release();//выходим из семафора 
        }

        private static void Eventstest()
        {
            var manulEvent = new ManualResetEvent(false);
            var autoResetEvent = new AutoResetEvent(false); 
            var threads = new Thread[10];

            for (int i = 0; i < threads.Length; i++)//создаем 10 потоков
            {
                threads[i] = new Thread(() =>
                 {


                     var threadId = Environment.CurrentManagedThreadId;
                     Console.WriteLine("Поток {0} создан и ждет разрешения на работу:{1:HH:mm:ss.ffff} ",threadId,DateTime.Now);//эти надписи появятся с разницой в 10 милисек

                     // при помощи WaitOne мы останавливаем наши потоки в одной точке чтобы потом мы могли сразу всех их запустить или завершить(все и сразу)
                     manulEvent.WaitOne();// тут мы поставили по типу флага котоырй нужно поднять по сути мы оставнавливаем все потоки в это точки чтобы потом все разом их заврешить
                    // autoResetEvent.WaitOne();// по сути тоже самое ( блокирует все потоки в одной точке) но при это нам каждый поток нужно запускать  вручную
                     Console.WriteLine("Поток {0} завершил свою работу:{1:HH:mm:ss.ffff} ", threadId, DateTime.Now);
                     manulEvent.Reset();//после того как все наши потоки выполнили нужное нам дейсвтие нам нужно сбрсить флаг
                 });
                threads[i].Start();// стартуем каждый поток 
                Thread.Sleep(100);

            }

            Console.WriteLine("Создание потока завершено");
            Console.ReadLine();

            Console.WriteLine("Потоком завершено выполнение ");
            manulEvent.Set();// а тут мы его выставляем 
            //autoResetEvent.Set();//отличие в том что он сам сбрасыват флаг
        }
        public static void Run()
        {
            Eventstest();

        }
    }
}
