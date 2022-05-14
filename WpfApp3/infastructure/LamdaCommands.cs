using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.infastructure.Base;

namespace WpfApp3.infastructure
{
    internal class LamdaCommands : Comands
    {
        private readonly Action<object> _Onexecute;
        private readonly Func<object,bool> _OnCanExecute;
        public LamdaCommands(Action<object> OnExecute, Func<object, bool> OnCanExecute = null)
        {
            _Onexecute = OnExecute;
            _OnCanExecute = OnCanExecute;
        }
        public override void Execute(object parameter)
        {
            _Onexecute(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter)&&(_OnCanExecute?.Invoke(parameter)??true);
        }
    }
}
