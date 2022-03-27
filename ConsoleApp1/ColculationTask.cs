using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ColculationTask<T>
    {
        private readonly Func<T> Calculation;
        private Thread _Thread;
        private T Result;
        public  T _Result
        {
            get
            {
                if(_Thread is null)
                {
                    Start();
                }
                _Thread.Join();//в начале дождаться выполнения потока а только затем  вернуть наш результат 
                return Result;
            }
        }
        public ColculationTask(Func<T> calculation)
        {
            Calculation = calculation;
        }

        /// <summary>
        /// мктод который будет создавать новый поток и запускать проццес расчета функции
        /// </summary>
        public void Start()
        {
            if (_Thread != null)
            {
                return;
            }
            _Thread = new Thread(() => Calc());
            _Thread.IsBackground = true;
            _Thread.Start();
        }
        private void Calc()
        {
            Result= Calculation();
        }
    }
}
