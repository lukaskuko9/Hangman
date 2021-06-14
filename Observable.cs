using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hangman
{
    public class Observable<T> : INotifyPropertyChanged
    {
        private T _value;
        public T Value 
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
