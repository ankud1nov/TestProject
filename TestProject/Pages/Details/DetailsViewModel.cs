using CommunityToolkit.Mvvm.ComponentModel;

namespace TestProject.Pages.Details
{
    public abstract partial class DetailsViewModel<T> : ObservableObject where T : class
    {
        private T _value;
        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value, nameof(Value));
        }
    }

}
