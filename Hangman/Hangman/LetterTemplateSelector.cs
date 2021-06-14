using System.Windows;
using System.Windows.Controls;

namespace Hangman
{
    public class LetterTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elemnt = container as FrameworkElement;
            LetterModel letter = item as LetterModel;
            if (letter.IsShown)
            {
                return elemnt.FindResource("ShownLetterDataTemplate") as DataTemplate;
            }
            else
            {
                return elemnt.FindResource("HiddenLetterDataTemplate") as DataTemplate;
            }
        }
    }
}
