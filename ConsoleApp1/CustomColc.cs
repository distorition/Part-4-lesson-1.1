using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   public static class CustomColc
    {
        public static void Run()
        {


            var calc = new ColculationTask<string>(() =>
            {
                Console.WriteLine("Процесс вычисления запущен");
                Thread.Sleep(3000);
                Console.WriteLine("Процесс завершен ");
                return "42";
            });

            calc.Start();
            var res = calc._Result;
            Console.WriteLine(" результат= {0}", res);
            Console.ReadLine();
        }
    }
}
