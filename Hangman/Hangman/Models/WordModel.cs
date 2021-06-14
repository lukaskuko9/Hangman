using System.Collections.ObjectModel;

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

            this[3].IsShown = true;
        }
    }
}
