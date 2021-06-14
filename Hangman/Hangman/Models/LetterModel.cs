using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hangman
{
    public class LetterModel
    {
        private const char _hiddenChar = '*';
        public char HiddenChar => _hiddenChar;

        public char Letter { get; set; }
        public bool IsShown { get; set; } = false;

        public LetterModel(char letter)
        {
            this.Letter = letter;
        }

        public override string ToString()
        {
            return $"{ Letter}";
        }
    }
}
