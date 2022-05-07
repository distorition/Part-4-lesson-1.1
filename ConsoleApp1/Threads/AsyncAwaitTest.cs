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
        private static async Task<int> ProcessMesageAsync(string Message)//наличие async еще не делает метод асинхронным 
        {
            //по сути мы таким образом разрываем наш метод на две части все что выше синхронно
            //await Task.Yield();//для того чтобы метод стал асинхронным у него должен быть как минимум один await
            //все что ниже аваит асинхронно 

            Console.WriteLine($"Запуск Процесса обработки {Message}");
            //Thread.Sleep(25);// в асинхронных методах не должно быть усыпления потока 
            await Task.Delay(10)//так же разрывает на две части только верхх будет выполнен в исходном потоке а низ уже будет выполнен поссле завершения паузы
            .ConfigureAwait(false);//таким образом продролжения будет выполнено в пуле потоков
           // .ConfigureAwait(true);// таким образом продолжение будет выполнено в оснвном потоке а точнее из того в котором мы начали (нужно д//)
            //await Task.Yield();//данный способ это читерство 
            Console.WriteLine($"Обработка сообщения завершена {Message}");
            // return Task.FromResult( Message.Length);//таким образом мы получаем задачу уэе завершенную 
            return Message.Length;
        }
        private static void PrintMesage(string Message)
        {
            Console.WriteLine($"Запуск Процесса обработки {Message}");
            Thread.Sleep(25);
            Console.WriteLine($"Обработка сообщения завершена {Message}");       
        }

        public static async Task RunAsync()
        {
            var message = "123";
            var processTask = Task.Run(() => ProcessMesage(message));
            Console.WriteLine("код выполняемый в главном потоке после запуска задачи ");
            try// во время ожидания задачи мы должны отследить возможность возникновения ошибки  
            {

                // все что выше await это синхронная часть выполнятся в том же потоке в которым мы начинали 
                // await processTask; //ПРАВИЛЬНАЯ РЕАЛИЗАЦИЯ ОЖИДАНИЯ ЗАВЕРШЕНИЕ ЗАДАЧИ!!
                var result = await processTask;//ПРАВИЛЬНАЯ РЕАЛИЗАЦИЯ ПОЛУЧЕНИЯ ЗНАЧЕНИЯ !!!

                //все что ниже await это асссинхронная часть ,задачи могут быть выполнены как в том же потоке так и в произвольном из пула потоков
            }
            catch (Exception r)
            {
                Console.WriteLine(r);
                throw;
            }
          
        }

        public static async Task Run2Async()//запуск паралельно асинхронно чего либо 
        {
            var message = Enumerable.Range(1, 100).Select(i =>$"message {i}");
            var TaskList = new List<Task<int>>();
            foreach (var item in message)
            {
                TaskList.Add(Task.Run(() => ProcessMesage(item)));
            }
           
           var result= await Task.WhenAll(TaskList);// мы передали наш список задач чтобы дождать завершения каждой задачи из списка


            //   var againResult = await Task.WhenAll(message.Select(i => Task.Run(() => ProcessMesage(i))));//этот вариант лучше в палне использования памяти и обработки ведь так метод When All сам будет регулировать в каком колличестве надо создать и запустить 
            var againREsult = await Task.WhenAll(message.Select(i => ProcessMesageAsync(i)));// такая оптимизация может работать медленне чем там что сверху
            Console.WriteLine($" сумма={againREsult .Sum()}");
        }
    }
}
