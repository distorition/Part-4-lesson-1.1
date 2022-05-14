using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp3.infastructure.Base
{
    public abstract class Comands : ICommand
    {
        public  event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// метод управляет активностью визуальных элементов 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter)=>true;// этот метод првоеряем можно ли выполнить дейстиве или нет
        //при возвоащения false кнопка у который передан данный метод будет не активна 


        public abstract void Execute(object parameter);//когда кто то счелкает по кнопке вызывается этот метод
    }
}
