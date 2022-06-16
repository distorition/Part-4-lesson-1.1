using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ReportsExamples.Infstructure
{
    internal static class FileaInfoEx
    {
       public static Process? Execute(this FileInfo file,bool UseShell=true)
        {
            var info=new ProcessStartInfo(file.FullName) { UseShellExecute = UseShell };//UseShellExecute заставит нашу систему самой подобрать способ запуска файла 
            return Process.Start(info);
        }
    }
}
