using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  public static  class ThreadPoolTest
    {
        public static void Run()
        {
        //    ThreadPool.SetMinThreads(4, 4); // при помщи этих методов мы можем регулировать кол-во потоков 
        //    ThreadPool.SetMaxThreads(32, 32);//первая цифра это сколько будет рабочих  вторая максимальное кол-во но обычно их с авят одинаково  
           var message = Enumerable.Range(1, 1000).Select(i => $"message{i}");
            foreach(var item in message)
            {
                ThreadPool.QueueUserWorkItem(x => ProcessMesage(item));//для запуска наших потоков паралельно 
            }
            Console.ReadLine();
        }

        private static void ProcessMesage(string Message, int Timeout = 250)
        {
            Console.WriteLine($"[id {Environment.CurrentManagedThreadId}] Запуск обработки {Message} ");
            if (Timeout > 0)
            {
                Thread.Sleep(Timeout);
            }
            Console.WriteLine($"[id {Environment.CurrentManagedThreadId}] процесс обработки {Message} в потоке  завершен");
        }
    }
}
