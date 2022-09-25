using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.infastructure.Base
{
    public class ShowMessageComands : Comands 
    {
        public override void Execute(object parameter)//для отображения сообщений 
        {
            if (parameter is null) return; 
            var mesage = parameter as string ?? parameter.ToString();
            MessageBox.Show(mesage,"Сообщение ",MessageBoxButton.OK,MessageBoxImage.Information);
        }
        public override bool CanExecute(object parameter)
        {

            if (parameter is null) return false;//если там null то наша команда не выолняется 
            var message = parameter as string ?? parameter.ToString();
            if (message.Length > 0) return true;
            return false;//если наше сообщения по длинне символлов равна 0 то тоже не выполняем 
        }
    }
}
