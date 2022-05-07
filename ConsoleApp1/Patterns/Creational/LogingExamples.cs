using ConsoleApp1;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   public static class LogingExamples
    {
        public static void Run()
        {
            var filePath = $"testConsole[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}]";
            var logger = new CombineLogger(ConsoleLogger.ConsolLog, FileLogger.Create(filePath));// мы создали комбинированный логер внутрь которого засунули консольный и файловый логеры
            var fileLoger=FileLogger.Create(filePath);
            var firsFaliLogger = logger.Logger.OfType<FileLogger>().First();
            //if(ReferenceEquals(fileLoger, firsFaliLogger))//эти два обьекта занимают одно и тоже места в памяти по сути это один и тот же обьект 
            //{
            //    logger.Log("Файловй логер был создан в диничном экземпляре");
            //}
            //logger.Log("123");
            //logger.Log("qwe");

            var message = Enumerable.Range(1, 100).Select(i => $"Message{i}");
            var fileLogersFactory = new FileLogerFactory();
            var debugLoggersFactory = new DebugLogerFactory();
            debugLoggersFactory.OutTo = DebugLogerFactory.LoggerType.Console;//при помощи изменения конфигурация мы можем выбирать какие логи будут формироваться 
            var rnd= new Random();

            var process = new MessageProcessor(rnd.NextDouble()>0.5?fileLogersFactory:debugLoggersFactory);//случайным образом мы выбираем либо fileLogersFactory либо debugLoggersFactory с вероятностью 0.5 
            process.Process(message);
        }

        private class MessageProcessor
        {
            public readonly IloggerFactory IloggerFactory;
            public MessageProcessor(IloggerFactory iloggerFactory)
            {
                IloggerFactory = iloggerFactory;
            }
            public void Process(IEnumerable<string> Message)
            {
                var loger = IloggerFactory.Create();
                foreach (var message in Message)
                {
                    loger.Log(message);  
                }
            }
           
        }
    }
    internal interface ILogger
    {
        void Log(string Message);
    }
    internal abstract class Logger : ILogger
    {
        public abstract void Log(string Message);
    }
    internal class ConsoleLogger : Logger
    {
        private static readonly object LoggerSyncRoot = new object();//создается обьект который не несет смысла но на нем будет выполнятся блокировка 

        private static ConsoleLogger? console;//вопросик перед именем значит что переменная может сожержать null  внутри себя

        // таким образом через свойства мы получам доступ к приватному конструктору класса
        //и таким образом при создании этого класса он бдует созда единожды и будет храниться в нашем свойстве и никто повторно его создать не сможет даже в многоптоке
        public static ConsoleLogger ConsolLog { get; } = new ConsoleLogger();//нужно оебспечить создание одного единственное экземпляра


        //шаблон одиночка с ленивой инциализацией (только если код не будет использоваться в многопоточности )
        public static ConsoleLogger Logs => console ?? new();//вопросики это if то есть если console сожержит null то мы переходим в право и создаем наш экземпляр 
        //{ это во что наше свойство разворачивается 

        //    get//оснновном программа происходит по этому пути 
        //    {
        //        if (console != null)// когда переменная не равна null
        //        {
        //            return console;// то мы получаем этот обьект 
        //        }
        //        console = new ConsoleLogger();// но при первым обращении его не будет и поэтому он будет создан
        //        return console;
        //    }
        //}

       /// <summary>
       /// шаблон одиночка с блокировкой (с защитой от многопоточности )
       /// </summary>
       public static ConsoleLogger ConsolLogs
        {
            get
            {
                if (console != null)// короткий путь который будет работать без блокировки чтобы не потреблять ресурсов на блокировку
                {
                    return console;
                }
                lock(LoggerSyncRoot)//блокируем критическую секцию
                {
                    if (console != null)// вторая проверка нужна для много поточности чтобы два потока не смогли создать два раза экземпляр этого класса
                    { 
                        return console;
                    }
                    console = new ConsoleLogger();
                    return console;
                }
            }
        }

        /// <summary>
        /// шаблон отложенно инциализации 
        /// </summary>
        private static readonly Lazy<ConsoleLogger> LazzyLogger = new Lazy<ConsoleLogger>(()=>new(),LazyThreadSafetyMode.ExecutionAndPublication);
        public static ConsoleLogger LoggerForLazzy => LazzyLogger.Value;

        private ConsoleLogger()// приватный конструктор не дает нам создвать наш класс то есть мы не сможем (new ConsoleLoger)
        {

        }
        public override void Log(string Message) => Console.WriteLine($"{DateTime.Now:yyy.M.ddTHH:mm:ss.fff}-{Message}");
      
    }
    internal class DebugLogger : Logger
    {
        public override void Log(string Message) => Debug.WriteLine($"{DateTime.Now:yyy.M.ddTHH:mm:ss.fff}-{Message}");//будет выводить логи в окно дебага 
    }
    internal class TraceLogger : Logger
    {
        public override void Log(string Message) => Trace.WriteLine($"{DateTime.Now:yyy.M.ddTHH:mm:ss.fff}-{Message}");//будет выводить логи в окно дебага 
    }

    internal class FileLogger : Logger
    { 
        private static readonly ConcurrentDictionary<string,FileLogger> FileLoggerCache = new ConcurrentDictionary<string,FileLogger>(StringComparer.OrdinalIgnoreCase/*без учета регистра*/);
        public static FileLogger Create(string FileName)//теперь при помщи этого метода мы можем постоянно создавать логи и никто не создат коснтруктор нашего класса
        {
            // если два потока одновеременно обратятся к слвоарю с одним и тем же файлом и логера не было то ваджды будет вызван метод (а нам это не надо)
          return  FileLoggerCache.GetOrAdd(FileName,path=>new FileLogger(FileName));//конкуретный словарь не гарантирует что это метод не будет вызван дважды 

        }

        private readonly string fileName;

        private FileLogger(string FileName)
        {
            fileName = FileName;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]// таким образом мы делаем блокировки этого метода чтобы туда мог войти только один поток 
        public override void Log(string Message)
        {

            File.AppendAllText(fileName, $"{DateTime.Now:s}-{Message}{Environment.NewLine}");
        }
    }

    internal class CombineLogger : Logger//с помощью CombineLogger сможем обьединять любой набор других логеров
    {
        private readonly ILogger[] Loggers;

        public IReadOnlyCollection<ILogger> Logger => Loggers;//нам это нужно ччтобы посмотреть какие логи были созданы 
        public CombineLogger(params ILogger[] loggers)
        {
            Loggers = loggers;
        }
        public override void Log(string Message)
        {
            foreach (var log in Loggers)
            {
                log.Log(Message);
            }
        }
    }
}
internal interface IloggerFactory
{
    ILogger Create();
}
internal abstract class LoggerFactore : IloggerFactory
{
    public abstract ILogger Create();
}

internal class DebugLogerFactory : LoggerFactore
{
    public enum LoggerType
    {
        Console,
        DebugOutput,
        Trace,
        
    }
    public LoggerType OutTo { get; set; }
    public override ILogger Create() => OutTo switch
    {
        //это новый способ написания свича

        LoggerType.Console => ConsoleLogger.Logs,
        LoggerType.DebugOutput => new DebugLogger(),
        LoggerType.Trace => new TraceLogger(),
        _ => throw new InvalidEnumArgumentException(nameof(OutTo), (int)OutTo, typeof(LoggerType)),

        //switch (OutTo)//это старый способ написания свича 
        //{
        //    default: throw new InvalidEnumArgumentException(nameof(OutTo), (int)OutTo, typeof(LoggerType));
        //    case LoggerType.Console: return ConsoleLogger.Logs;
        //    case LoggerType.DebugOutput: return new DebugLogger();
        //    case LoggerType.Trace: return new TraceLogger();

        //}
    };
}
internal class FileLogerFactory : LoggerFactore
{
    public override ILogger Create() => FileLogger.Create("test-log-log");
    
}
