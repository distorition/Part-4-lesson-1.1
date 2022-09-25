using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.infastructure.Base
{
    internal class ShowMessageComands : Comands 
    {
        public override void Execute(object parameter)//для отображения сообщений 
        {
            if (parameter is null) return;
            var mesage = parameter as string ?? parameter.ToString();
            MessageBox.Show(mesage,"Сообщение ",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
