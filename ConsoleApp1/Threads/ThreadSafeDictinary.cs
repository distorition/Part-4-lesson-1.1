using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;

namespace ConsoleApp1
{
    internal static class ThreadSafeDictinary
    {
        public static void Run()
        {
            var strings = Enumerable.Range(1, 1000).Select(i => $"Strings-{i % 54}");

          //  var hashPool = new ConcurrentDictionary<string, byte[]>();//расчитываем хеш сумму
            foreach( var item in strings)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    //таким образом мы расчитаем лишь те значения которые у нас будут то есть для всех 54 строк 
                    var hash = ComputeHashCod(item); // мы берем значения по ключу если оно там есть если нет то рачитываем его
                                                                            // если у словаря не будет значения то он будет его создавать при помощи ComputeHash
                    Console.WriteLine("{0} : {1}",item,Convert.ToBase64String(hash));
                });
            }
            Console.ReadLine();
        }

        private static readonly ConcurrentDictionary<string, byte[]> HashBufer = new ConcurrentDictionary<string, byte[]>();
        private static byte[] ComputeHashCod(string s)
        {
            return HashBufer.GetOrAdd(s, x => ComputeHash(x));
        }



        private static byte[] ComputeHash(string s)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));
            if (s.Length == 0) return new byte[32];

            Thread.Sleep(1000);
            var bytes = Encoding.UTF8.GetBytes(s);
            //директива юзинг работает только с теми обьектами которые поодерживают интерфейс IDisposed
            // алгоритм хеишрования MD5 для рачета хеш суммы
            using (var hasher = MD5.Create())//директива юзинг нужна для очистки некотролируемый памяти то есть чтобы когда мы выполнили операцию мы осоводили память
            {
               return hasher.ComputeHash(bytes);//преобразаует в хеш код
            }
        }
        
    }
}
