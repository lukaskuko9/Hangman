using System;
using System.Collections.Generic;
using System.Linq;
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
        public delegate void LetterClicked(LetterControl letterConrol);
        public event LetterClicked OnLetterClick;
        public List<LetterControl> Letters { get; set; }

        public static DependencyProperty LetterMinWidthProperty = DependencyProperty.Register(nameof(LetterMinWidth), typeof(int), typeof(LetterControl));
        public int LetterMinWidth
        {
            get { return (int)GetValue(LetterMinWidthProperty); }
            set { SetValue(LetterMinWidthProperty, value); }
        }

        public Keyboard()
        {
            InitializeComponent();
            IsEnabledChanged += Keyboard_IsEnabledChanged;
            SizeChanged += Keyboard_SizeChanged;
        }

        private void Keyboard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SecondRow.Margin = new Thickness((Letters[0].ActualWidth+5)/2, 0, (Letters[0].ActualWidth + 5) / 2, 0);
            this.ThirdRow.Margin = new Thickness((Letters[0].ActualWidth+5)/2, 0, (Letters[0].ActualWidth + 5) / 2, 0);
        }

        private void Keyboard_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Letters.ForEach(l => l.IsEnabled = (bool)(e.NewValue));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Letters = FindVisualChildren<LetterControl>(this).ToList();
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public void PressLetter(char letter)
        {
            Letters.First(l => l.Letter == letter).PressLetter();
        }

        private void LetterControl_OnLetterClick(LetterControl letterControl)
        {
            OnLetterClick?.Invoke(letterControl);
        }
    }
}
