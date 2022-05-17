using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class LamdaExpression
    {
        public static void Run()
        {
            string str = "asda";
            int i = 43;
            List<int> list = new List<int>();
            Student student = new Student();
            StudentDelegate studentDelegate = Process;//в делегат мы можем присвоить метод

            PorcessStudent(Enumerable.Empty<Student>(), Foo);

            //Action это процедура которая ничего не возвращает то есть void 
            Action<Student> action = Foo;//стандартный дилегат ( который чаще всего используют), в угловых скобочках указывают параметры , то есть то что передается в метод который мы вызвали через делегат 

            //если нам нужно возвращаемое значение то используем функцию
            Func<Student, int> func = s => s.Name.Length;//первый параметр входной , второй выходной то есть ( public int func( Student student))

            Func<double,double> sin=x=>Math.Sin(x);//в начале будет расчитываться синус 
            Func<double,double> cos=x=>Math.Cos(x);//потом косинус 

            Func<double, double> sum_sun_cos=Sum(sin,cos);// а только потом будет их сумма 

            var result = sum_sun_cos(Math.PI / 3);// все значения будут расчитыватся от значения ПИ/3

        }


        /// <summary>
        /// Функциональное программирование 
        /// </summary>
        /// <param name="a"> эта переменная нашей первой функции(метода) в которой происходят какие либо действия(расчеты) </param>
        /// <param name="b">эта переменная нашей второй функции(метода) в которой происходят какие либо действия(расчеты) </param>
        /// <returns></returns>
        public static Func<double,double>Sum(Func<double,double> a,Func<double,double> b)
        {
            return x=>a(x)+b(x);
        }


      public  delegate void StudentDelegate(Student student);//таким образом мы создаем делегаты , они работаю только если в методе первая  строка то есть (возвращаемы тип , принимаемые значения, доступ) одинаковые 
        public static void PorcessStudent(IEnumerable<Student> students,StudentDelegate Delegate)//таким образом мы помещаем метод в метод
        {
            foreach (var item in students)
            {
                Delegate(item);
            }
        }
        public static void Process(Student student)
        {
            Console.WriteLine(student);
        }
        public static void Foo(Student student)
        {
            Console.WriteLine(student);
        }
        public static string Foo1(Student student)
        {
            Console.WriteLine(student);
            return "1";
        }
    }
}
