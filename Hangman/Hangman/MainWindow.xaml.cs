﻿using System;
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
        public WordModel Word { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Word = new WordModel("random");
        }

        private void NewWord_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Word.SetNewWord("new word");
        }

        private void Keyboard_OnLetterClick(char letter)
        {
            Word.ShowLetter(letter);
        }
    }
}
