using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp3.infastructure;
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
        public ICommand ShowCaomandMessage => _ShowMessageCommadn ??= new LamdaCommands(OnShowMessageCommandExeption, CanShowMessageCommandExeption);
        private bool CanShowMessageCommandExeption(object parametr) => true;
        private void OnShowMessageCommandExeption(object parametr)
        {
            if (parametr is null) return;
            var message= parametr as string??parametr.ToString();
            MessageBox.Show(message,"Сообщение без модели ",MessageBoxButton.OK,MessageBoxImage.Information);
        }

    }
}
