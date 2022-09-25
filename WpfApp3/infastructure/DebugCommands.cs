using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp3.infastructure.Base;

namespace WpfApp3.infastructure
{
    public class DebugCommands : Comands
    {
        private readonly ICommand _BaseCommand;

        public DebugCommands(ICommand command)
        {
            _BaseCommand = command;
        }
        public override void Execute(object parameter)
        {
            var timer=Stopwatch.StartNew();
            Debug.WriteLine("Команда {0} Запущена ",_BaseCommand);//отправляет свединя в окно дебага 

            _BaseCommand.Execute(parameter);
            Debug.WriteLine("Команда {0} Выпоолнена за {1} mc ", _BaseCommand,timer.ElapsedMilliseconds);
        }
        public override bool CanExecute(object parameter)
        {
            Debug.WriteLine("запрос возможности выполнения команды {0}", _BaseCommand);
            var result= _BaseCommand.CanExecute(parameter);
            Debug.WriteLine(" командe {0} можно выполнить -{1}", _BaseCommand,result);
            return result;
        }
    }
}
