using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hangman
{
    public class Observable<T> : INotifyPropertyChanged
    {
        private List<string> _toNotify = new List<string>();

        private T _value;
        public T Value 
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
                _toNotify.ForEach(n =>
                    OnPropertyChanged(n)
                );
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

        public void AddNotifyOnChange(string name)
        {
            _toNotify.Add(name);
        }
    }
}
