using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var currentThread = Thread.CurrentThread;
            currentThread.Priority = ThreadPriority.Highest;

            //while (true)
            //{
            //    Console.Title = DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss:ffff");
            //    Thread.Sleep(100);
            //}

            //ThreadTest.Run();
            //CriticalSection.Run();
            //  SynchornizationTest.Run();
            //  ThreadPoolTest.Run();
            ParalelTest.Run();
            //ThreadSafeDictinary.Run();
        }
    }
}
