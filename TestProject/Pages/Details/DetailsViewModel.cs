using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows;
using System.Windows.Input;
using TestProject.DataAccess;
using TestProject.Messages;

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
            //TODO: Добавить проверки перед сохранением
            try
            {
                if (_value is Domain.Entities.Employee)
                {
                    OnPropertyChanged("FullName");
                }
                var context = ServicesLocator.Current.GetRequiredService<DBContextDataAccess>();
                context.SaveOrUpdate(Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении");
            }

            //TODO: можно сделать выборочное обовление дерева но пока так
            StrongReferenceMessenger.Default.Send(new NeedRefreshMessage());
        }

        private void Delete()
        {
            try
            {
                var context = ServicesLocator.Current.GetRequiredService<DBContextDataAccess>();
                context.Delete(Value);
            }
            catch (Exception)
            {
                MessageBox.Show($"Ошибка при удалении");
            }
            StrongReferenceMessenger.Default.Send(new NeedRefreshMessage());
        }
    }

}
