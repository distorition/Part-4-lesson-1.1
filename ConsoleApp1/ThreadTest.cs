using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   internal static class ThreadTest
    {
        /// <summary>
        /// в этом методе выплняется главный поток 
        /// </summary>
        public static void Run()
        {
            
            var timerThread = new Thread(()=>TimerUpdate());// тут мы создаем поток который будет запусскать наш метод 
            timerThread.Priority = ThreadPriority.BelowNormal;
            timerThread.Name = "поток часов";
            timerThread.IsBackground = true;//тут мы делаем наш поток фоновым и таким образом при завршении программы наш поток тоже будет завершаться 
            timerThread.Start();//только после того как мы запустим наш поток он начнет работать 
            Console.WriteLine("ВВод пользователя");
            Console.ReadLine();

            timerThread.Interrupt();//данный метод сейчас считается не безопасным 


            //тот тайм аут который мы передали в метод джоин должен совпадать или быть чуть больше чем  тот который установлен в нутри потока в данном случаем это TimerUpdate и его Thread.Sleep(250)
           if(! timerThread.Join(300))//таким образом мы заставялем наш главный поток ждать пока не завершиться вторичный , но в таком случаем мы можем получить мертвую блакировку ибо первый поток никогда не закончиться а второй будет его ждать
            {
                timerThread.Interrupt();//если у нас не удалось ссинхронизироваться с нашим потоком то мы останавливаем его жестко
                timerThread.Join();//таким образом после оставновки мы гарантированно с ним синхронизируемся 
            }

            Console.WriteLine("программа завершена");
        }

        /// <summary>
        /// тут выполняется вторичный поток 
        /// </summary>
        private static void TimerUpdate()
        {
            try
            {


                while (true)
                {
                    Console.Title = DateTime.Now.ToString("dd.mm.yyyy HH: mm:ss: ffff");
                    Thread.Sleep(250);
                }
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine("Поток преврав мягко ",e);
            }
        }
    }
}
