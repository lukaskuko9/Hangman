using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void Game();
        public event Game OnGameStart;
        public event Game OnGameEnd;
        public event Game OnGameWin;
        public event Game OnGameLose;

        public WordModel Word { get; set; } = new WordModel("new word");

        public Observable<int> Lives { get; set; } = new Observable<int>();
        public Observable<bool> GameBeingPlayed { get; set; } = new Observable<bool>();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Word.OnCorrectGuess += Word_OnCorrectGuess;
            Word.OnIncorrectGuess += Word_OnIncorrectGuess;
            OnGameWin += Game_OnGameWin;
            OnGameLose += Game_OnGameLose;
            OnGameStart += MainWindow_OnGameStart;
            OnGameEnd += MainWindow_OnGameEnd;
            OnGameStart?.Invoke();
        }

        private void MainWindow_OnGameEnd()
        {
            GameBeingPlayed.Value = false;
        }

        private void MainWindow_OnGameStart()
        {
            Lives.Value = 3;
            GameBeingPlayed.Value = true;
        }

        private void Word_OnIncorrectGuess()
        {
            if (--Lives.Value <= 0)
                OnGameLose?.Invoke();
        }

        private void Word_OnCorrectGuess()
        {
            bool gameWon = Word.ToList().TrueForAll(l => l.IsShown == true);
            if (gameWon)
            {
                OnGameWin?.Invoke();
            }
        }

        private void Game_OnGameLose()
        {
            MessageBox.Show("YOU LOST!");
            OnGameEnd?.Invoke();
        }

        private void Game_OnGameWin()
        {
            MessageBox.Show("YOU WON!");
            OnGameEnd?.Invoke();
        }

        private void NewWord_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Word.SetNewWord("new word");
            OnGameStart?.Invoke();
        }

        private void Keyboard_OnLetterClick(char letter)
        {
            Word.ShowLetter(letter);
        }
    }
}
