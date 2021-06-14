using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hangman
{
    public class LetterModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private const char _hiddenChar = '*';
        private char letter;
        private bool isShown;

        public char HiddenChar => _hiddenChar;

        public char Letter
        {
            get => letter;
            set
            {
                letter = value;
                OnPropertyChanged();
            }
        }
        public bool IsShown 
        { 
            get => isShown;
            set
            {
                isShown = value;
                OnPropertyChanged();
            }
        }

        public LetterModel(char letter)
        {
            this.Letter = letter;
            if (string.IsNullOrWhiteSpace(letter.ToString()))
                this.IsShown = true;
        }

        public override string ToString()
        {
            return $"{ Letter}";
        }
    }
}
