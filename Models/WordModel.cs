using System.Collections.ObjectModel;
using System.Linq;

namespace Hangman
{
    public class WordModel : ObservableCollection<LetterModel>
    {
        public delegate void HasGuessed();
        public event HasGuessed OnCorrectGuess;
        public event HasGuessed OnIncorrectGuess;

        public string WordStr { get; private set; }
        public WordModel(string wordStr)
        {
            this.WordStr = wordStr;
            SetNewWord(this.WordStr);
        }

        public void SetNewWord(string wordStr)
        {
            this.Clear();
            this.WordStr = wordStr;

            for (int i = 0; i < wordStr.Length; i++)
                Add(new LetterModel(wordStr[i]));
        }

        public void ShowLetter(char letter)
        {
            var matchedLetters = Items.ToList()
                 .Where(l => l.Letter.ToString().ToUpper().First() == letter.ToString().ToUpper().First())
                 .ToList();

            if (matchedLetters.Count == 0)
                OnIncorrectGuess?.Invoke();
            else
            {
                matchedLetters.ForEach(l => l.IsShown = true);
                OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
                OnCorrectGuess?.Invoke();
            }

        }
    }
}
