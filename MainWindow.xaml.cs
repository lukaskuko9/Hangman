using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int MaxLives = 6;

        public delegate void Game();
        public event Game OnGameStart;
        public event Game OnGameEnd;
        public event Game OnGameWin;
        public event Game OnGameLose;

        public WordModel Word { get; set; }
        public Observable<int> Lives { get; set; } = new Observable<int>();

        public List<string> AvailableWords;
        public Observable<bool> GameBeingPlayed { get; set; } = new Observable<bool>();
        public Observable<Visibility> RealWordVisibility { get; set; } = new Observable<Visibility>()
        {
            Value = Visibility.Hidden
        };


        public string HangmanImgPath => $"pack://application:,,,/Assets/Images/{MaxLives - Lives.Value +1}.png";

        public Observable<ImageSource> ImageSource { get; set; } = new Observable<ImageSource>();

        public string GetEmbeddedResource(string namespacename, string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = namespacename + "." + filename;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            HangmanBg.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/hangmanBg.png"));
            AvailableWords = GetEmbeddedResource("Hangman", "Assets.words.txt").Split('\n').ToList();

            string wordStr = GetRandomWord();
            Word = new WordModel(wordStr);
            Word.OnCorrectGuess += Word_OnCorrectGuess;
            Word.OnIncorrectGuess += Word_OnIncorrectGuess;
            OnGameWin += Game_OnGameWin;
            OnGameLose += Game_OnGameLose;
            OnGameStart += MainWindow_OnGameStart;
            OnGameEnd += MainWindow_OnGameEnd;

            OnGameStart?.Invoke();

            KeyDown += MainWindow_KeyDown;
            
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString().Length == 1) //only keys from A-Z have length one when converted to string :)
                KeyboardControl.PressLetter(e.Key.ToString()[0]);
        }

        private string GetRandomWord()
        {
            int index = new Random().Next(0, AvailableWords.Count);
            return AvailableWords[index];
        }

        private void MainWindow_OnGameEnd()
        {
            Word.RevealWord();
            RealWordVisibility.Value = Visibility.Visible;
            GameBeingPlayed.Value = false;
        }

        private void MainWindow_OnGameStart()
        {
            Lives.Value = MaxLives;
            RealWordVisibility.Value = Visibility.Hidden;
            GameBeingPlayed.Value = true;
            UpdateHangmanImg();
        }

        private void UpdateHangmanImg()
        {
            HangmanImg.Source = new BitmapImage(new Uri(HangmanImgPath));
        }

        private void Word_OnIncorrectGuess()
        {
            --Lives.Value;

            UpdateHangmanImg();

            if (Lives.Value <= 0)
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

        private void Keyboard_OnLetterClick(LetterControl letterControl)
        {
            Word.ShowLetter(letterControl.Letter);
        }

        private void NewWord_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string newWord = GetRandomWord();
            Word.SetNewWord(newWord);
            OnGameStart?.Invoke();
        }

        private void About_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Author: Lukáš Machajdík\nRepository:https://github.com/lukaskuko9/Hangman", "About", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
