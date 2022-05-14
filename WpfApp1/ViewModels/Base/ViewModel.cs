using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged// что все работало быстро и не жрало много памяти нам нужно реализовать этот интерфейс
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChange([CallerMemberName]string propertyName=null)//при помощи атрибута  компилятор сам будет вставлять в метод названия свойства из  которого он был вызван
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool Set<T>(ref T field,T value,[CallerMemberName] string PropetyNme = null)//метод чтобы еще больше уменьшить наш код
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChange(PropetyNme);
            return true;
        }
    }
}
