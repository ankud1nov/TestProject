using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace TestProject.Pages.Details
{
    //Сделал для уменшения количества кода, на реальном проекте сделал бы отдельную модель для отображения (компании, сотрудника, подразделения)
    public abstract partial class DetailsViewModel<T> : ObservableObject where T : class
    {
        private T _value;
        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value, nameof(Value));
        }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        protected DetailsViewModel(T value)
        {
            _value = value;
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Save()
        {
            MessageBox.Show($"save");
        }

        private void Delete()
        {
            MessageBox.Show($"Delete");
        }
    }

}
