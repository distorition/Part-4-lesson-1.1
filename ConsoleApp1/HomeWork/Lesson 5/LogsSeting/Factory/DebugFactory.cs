using ConsoleApp1.HomeWork.Lesson_5.InterfaceLoger;
using ConsoleApp1.HomeWork.Lesson_5.Loggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5.Factory
{
    internal class DebugFactory : LogerFactory
    {
        public enum LogLevel
        {
            Console,
            Debug,
            Trace
        }
        public LogLevel OutLogs { get; set; }
        public override ILoger Create() => OutLogs switch
        {
            LogLevel.Console =>ConsoleLogs.Logs,
            LogLevel.Debug => new DebgLogger(),
            LogLevel.Trace => new TrceLogger(),
            _ => throw new InvalidEnumArgumentException(nameof(OutLogs), (int)OutLogs, typeof(LogLevel)),
        };
    }
    internal class DebgLogger : ForLogs
    {
        public override void Log(string Message) => Debug.WriteLine($"{DateTime.Now:yyy.M.ddTHH:mm:ss.fff}-{Message}");//будет выводить логи в окно дебага 
    }
    internal class TrceLogger : ForLogs
    {
        public override void Log(string Message) => Trace.WriteLine($"{DateTime.Now:yyy.M.ddTHH:mm:ss.fff}-{Message}");//будет выводить логи в окно дебага 
    }
    internal class FileLogerFactory : LogerFactory
    {
        public override ILoger Create() => FilesLoggers.Create("test-log-log");

    }
}
