using System.Collections.ObjectModel;
using System.Linq;

namespace Hangman
{
    public class WordModel : ObservableCollection<LetterModel>
    {
        private string wordStr;
        public WordModel(string wordStr)
        {
            this.wordStr = wordStr;
            SetNewWord(this.wordStr);
        }

        public void SetNewWord(string wordStr)
        {
            this.Clear();
            this.wordStr = wordStr;

            for (int i = 0; i < wordStr.Length; i++)
                Add(new LetterModel(wordStr[i]));
        }

        public void ShowLetter(char letter)
        {
            Items.ToList()
                 .Where(l => l.Letter.ToString().ToUpper().First() == letter.ToString().ToUpper().First())
                 .ToList()
                 .ForEach(l => l.IsShown = true);

            OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }
    }
}
