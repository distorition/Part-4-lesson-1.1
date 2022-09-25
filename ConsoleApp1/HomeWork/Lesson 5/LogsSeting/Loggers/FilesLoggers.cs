using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5.Loggers
{
    internal class FilesLoggers : ForLogs
    {
        private static readonly ConcurrentDictionary<string, FilesLoggers> _filesLoggers = new ConcurrentDictionary<string,FilesLoggers>(StringComparer.OrdinalIgnoreCase);
        public static FilesLoggers Create(string FileName)
        {

            return _filesLoggers.GetOrAdd(FileName, path => new FilesLoggers(FileName));
        }
        private readonly string _FileName;

        public FilesLoggers(string fileName)
        {
            _FileName = fileName;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Log(string Message)
        {
            File.AppendAllText(_FileName,$"{DateTime.Now:s}-{Message}{Environment.NewLine}");
        }
    }
}
