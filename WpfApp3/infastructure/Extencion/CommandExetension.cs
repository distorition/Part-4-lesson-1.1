using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp3.infastructure.Extencion
{
    internal static class CommandExetension
    {
        public static ICommand Debug(this ICommand command)
        {
            if(command is DebugCommands) return command;
            return new DebugCommands(command);
        }
    }
}
