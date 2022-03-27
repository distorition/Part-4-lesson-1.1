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
            var message = Enumerable.Range(1, 1000).Select(i => $"message{i}");
            foreach(var item in message)
            {
                ThreadPool.QueueUserWorkItem(x => ProcessMesage(item)); 
            }
            Console.ReadLine();
        }

        private static void ProcessMesage(string Message, int Timeout = 250)
        {
            Console.WriteLine($"Запуск обработки {Message} в потоке {Environment.CurrentManagedThreadId}");
            if (Timeout > 0)
            {
                Thread.Sleep(Timeout);
            }
            Console.WriteLine($"процесс обработки {Message} в потоке {Environment.CurrentManagedThreadId} завершен");
        }
    }
}
