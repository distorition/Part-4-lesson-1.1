using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   internal static class ParalelTest
    {
        public static void Run()
        {
            var message = Enumerable.Range(1, 300).Select(i => $"Message-{i}");

            var timer = Stopwatch.StartNew();//таймер для замерки времени 

                                                
            var totalLenght = message.AsParallel() //эта часть будет паралельна
                .WithDegreeOfParallelism(12)//тут мы указываем в сколькх потоках это бдует выполнятся
                .Select(m => ProcessMesage(m))
                .AsSequential()//это уже бдет последовательно
                .Sum();//при помощи метода AsParalel мы вклчюаем разпаралеливание и таким образом увеличили быстродейсвтие 


            Parallel.Invoke();//позволяет паралельно выполнить несколько дейсвтий 
            Parallel.For(5, 100, x => Console.WriteLine("123-{0}",x));//иммитирует паралельную работу цикла 
            Parallel.ForEach(message, x => ProcessMesage(x));

            timer.Stop();
            Console.WriteLine($"Обработка сообщений завершена за {timer.Elapsed} ");
        }

        private static int ProcessMesage(string Message)
        {
            Console.WriteLine($"Запуск Процесса обработки {Message}");
            Thread.Sleep(25);
            Console.WriteLine($"Обработка сообщения завершена {Message}");
            return Message.Length;
        }
    }
}
