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
    /// Interaction logic for LetterControl.xaml
    /// </summary>
    public partial class LetterControl : UserControl
    {
        public delegate void LetterClicked(LetterControl LetterControl);
        public event LetterClicked OnLetterClick;

        public static DependencyProperty LetterProperty = DependencyProperty.Register(nameof(Letter), typeof(char), typeof(LetterControl));
        public char Letter
        {
            get { return (char)GetValue(LetterProperty); }
            set { SetValue(LetterProperty, value); }
        }

        public LetterControl()
        {
            InitializeComponent();
            DataContext = this;
            OnLetterClick += DisableLetter_OnLetterClick;
        }

        private void DisableLetter_OnLetterClick(LetterControl LetterControl)
        {
            this.IsEnabled = false;
        }

        public void PressLetter()
        {
            OnLetterClick?.Invoke(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnLetterClick?.Invoke(this);
        }
    }
}
