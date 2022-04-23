using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static/* async Task */void Main(string[] args)
        {

            //var currentThread = Thread.CurrentThread;
            //currentThread.Priority = ThreadPriority.Highest;

            //while (true)
            //{
            //    Console.Title = DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss:ffff");
            //    Thread.Sleep(100);
            //}

            //ThreadTest.Run();
            //CriticalSection.Run();
            //  SynchornizationTest.Run();
            //  ThreadPoolTest.Run();
            // ParalelTest.Run();
            //ThreadSafeDictinary.Run();
            //var ttask= AsyncAwaitTest.RunAsync();// таким образом если выскочит ошибка то она будет хранится в переменной и не положит нашу программу 
            // await AsyncAwaitTest.Run2Async();
            LogingExamples.Run();
           
        }
    }
}
