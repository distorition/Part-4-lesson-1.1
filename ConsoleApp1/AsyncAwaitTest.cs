using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   public static class AsyncAwaitTest
    {
        public static void Run()
        {
            //Task task = new Task(() => PrintMesage("123")); так делать не надо 
            //task.Start();

            // так формируется задача пэтому можем поместить её в переменную 
          var printMessage=  Task.Run(() => PrintMesage("234"));//этот метод будет выполнен в пуле потоков ( паралельно)

            // при помощи ContinueWith так же формируется новая задача 
           var anothetrtask= printMessage.ContinueWith(t => Console.WriteLine("задача завершена"));// таким образом мы сможем что ли запустить сразу же после заврешения нашей задачи 
            anothetrtask.ContinueWith(t => Console.WriteLine("или нет.."));//и мы так же можем прицепить продолжение 
            try
            {
                //НЕПРАВИЛЬНО!!
                //но при этом таким методы ожидания приводят к мертвой блокировке 

                printMessage.Wait();//при помощи этого метода мы можем дождать выполнения нашей операции 
                // так же при помщи метода try cath та ошибка которая возникнет внутри метода мы сможем её перехватить
            }
            catch (Exception)
            {

                throw;
            }


            //задачи бывают разные с возращаемым значения так и без
            var processTask = Task.Run(() => ProcessMesage("123"));// САМЫЙ ПРОСТОЙ СПОСОБ СОЗДАТЬ ЗАДАЧА ЧТОБЫ ЗАПУСТИТЬ ЕЁ АСИНХРОННО И ПАРАЛЕЛЬНО 

            try
            {//НЕПРАВИЛЬНО!!
                //но при этом таким методы ожидания приводят к мертвой блокировке 

                var processResult = processTask.Result;//таким образом мы заблокирвоали нашу задачу пока она не заврешиться чтобы получить её значения (либо ощибку)
            }
            catch (Exception)
            {

                throw;
            }
         
            var longRunning = Task.Factory.StartNew(() => ProcessMesage("123"),TaskCreationOptions.LongRunning);//это если наша задача будет долго обрабатываться (если она большая ) то таким образом мы запустим ее в отдельном потоке 

        }

        
        private static int ProcessMesage(string Message)
        {
            Console.WriteLine($"Запуск Процесса обработки {Message}");
            Thread.Sleep(25);
            Console.WriteLine($"Обработка сообщения завершена {Message}");
            return Message.Length;
        }
        private static void PrintMesage(string Message)
        {
            Console.WriteLine($"Запуск Процесса обработки {Message}");
            Thread.Sleep(25);
            Console.WriteLine($"Обработка сообщения завершена {Message}");       
        }
    }
}
