using System;
using System.Collections.Generic;
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

namespace Lorem_Ipsum_Generator
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        Generator gen;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            gen = new Generator();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            string text = box.Text;

            foreach(Char c in text)
            {
                if(!Char.IsNumber(c))
                {
                    text = text.Remove(text.IndexOf(c), 1);
                }
            }

            box.Text = text;
            box.CaretIndex = text.Length;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string wordsBoxText = txtWords.Text;
            string paragraphsBoxText = txtParagraphs.Text;

            if(!String.IsNullOrEmpty(wordsBoxText) && !String.IsNullOrEmpty(paragraphsBoxText))
            {
                int words = Convert.ToInt32(wordsBoxText);
                int paragraphs = Convert.ToInt32(paragraphsBoxText);

                if (words > 0 && paragraphs > 0)
                {
                    txtResultBox.Text = gen.Generate(words, paragraphs);
                }
            }
        }
    }
}
