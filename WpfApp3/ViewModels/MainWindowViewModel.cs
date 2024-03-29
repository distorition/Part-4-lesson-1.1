﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp3.infastructure;
using WpfApp3.infastructure.Extencion;
using WpfApp3.ViewModels.Base;

namespace WpfApp3.ViewModels
{
    /// <summary>
    /// Лоигка модели представляения 
    /// они обычно регулируют визуальную  часть 
    /// </summary>
    public class MainWindowViewModel : ViewModel//INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        private string _title="Главное окно программы ";

       public string Title
        {
            get { return _title; }
            set=>Set(ref _title, value);
            //set {
            //    if(_title == value) return;//если значения одинаковые то ничего делать не надо
            //    _title = value;
            //    OnPropertyChange();//теперь нам больше не требуется вставлять названия свойвства ибо есть атрибут внутри этого метода который сам встваляет названия свойства из которого его вызывают
            //    OnPropertyChange(nameof(Title));
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));//при помощи этого свойтсва мы сможем сразу же показывать изменения который были с нашем полем 
            //   TitleChange?.Invoke(this,  EventArgs.Empty);
            //}
        }
        // public event EventHandler TitleChange;

        private string _description=null;
        public string Description { get => _description; set => Set(ref _description, value); }
        private ICommand? _ShowMessageCommadn;
        public ICommand ShowCaomandMessage => _ShowMessageCommadn ??= new LamdaCommands(OnShowMessageCommandExeption, CanShowMessageCommandExeption).Debug();
        private bool CanShowMessageCommandExeption(object parametr)//при помощи этого метода будем давать разрешения на выполнения команды 
        {
            if (parametr is null) return false;//если там null то наша команда не выолняется 
            var message = parametr as string ?? parametr.ToString();
            if(message.Length > 0) return true;
            return false;//если наше сообщения по длинне символлов равна 0 то тоже не выполняем 
        }
        private void OnShowMessageCommandExeption(object parametr)
        {
            if (parametr is null) return;
            var message= parametr as string??parametr.ToString();
            MessageBox.Show(message,"Сообщение без модели окна",MessageBoxButton.OK,MessageBoxImage.Information);
        }

    }
}
