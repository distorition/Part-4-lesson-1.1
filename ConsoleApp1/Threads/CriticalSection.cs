using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  internal static  class CriticalSection
    {
        public static void Run()
        {
            var threadLis = new List<Thread>();

            for (int i = 0; i < 10; i++)
            {
                var loacl = i;//при помощи локальной переменной мы избавляемся от замыкания цикла 
                var thread = new Thread(() => Print($"Message-{loacl}", 10));
                thread.IsBackground = true;// мы делаем его фоновым чтобы он не мешал нашему основному потоку при его заврешении и таким образом мы сможем завершить нашу прогрумму 
                //не дожидаясь когда этот поток закончит свое дейсвтие ибо он завершиться вместе с ней а
                threadLis.Add(thread);
                thread.Start();
            }
            Console.ReadLine();
        }

        private static readonly object SyncRoot = new object();// при помщи этой переменной мы блокируем способы прерывания нашего потока и таким образом другой поток не сможет в него войти
        private static void Print(string MEssage, int count , int Timeout = 200)
        {
            for (int i = 0; i < count; i++)
            {
                if (Timeout > 0)
                {
                    Thread.Sleep(Timeout);
                }
                lock (SyncRoot)//при помощи лок мы защищаем нашу критическую секцию от сразу нескольких чтений потоками
                {
                    #region Критическая секция это так секция в которую обращаются сразу несколько потоков 
                    Console.Write("ThreadID : {0}", Environment.CurrentManagedThreadId);
                    Console.Write("[{0}]", i);
                    Console.WriteLine(MEssage);
                    #endregion
                }
            }
        }

        public static void Run2()
        {
            var list = new List<string>();// место куда слажиываем наши сообщения 
            for (int i = 0; i < 10; i++)
            {
                var loc = i;
                var thread = new Thread(() =>
                 {
                     var threadId = Environment.CurrentManagedThreadId;

                     for (int j = 0; j < 50; j++)
                     {
                         var message = $"[{j} ]Message-{j} form threadf {threadId}";
                         lock (list)//чем меньше обьектов мы включим в нашу критиескую секциию тем быстрее она будте работать
                         {
                             list.Add(message);
                         }
                     }
                 });
                thread.IsBackground = true;
                thread.Start();
            }
            Console.ReadLine();
        }
    }
}
