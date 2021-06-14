using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class Keyboard : UserControl
    {
        public delegate void LetterClicked(char letter);
        public event LetterClicked OnLetterClick;

        public static DependencyProperty LetterMinWidthProperty = DependencyProperty.Register(nameof(LetterMinWidth), typeof(int), typeof(LetterControl));
        public int LetterMinWidth
        {
            get { return (int)GetValue(LetterMinWidthProperty); }
            set { SetValue(LetterMinWidthProperty, value); }
        }

        public Keyboard()
        {
            InitializeComponent();
        }

        private void LetterControl_OnLetterClick(char letter)
        {
            OnLetterClick?.Invoke(letter);
        }
    }
}
