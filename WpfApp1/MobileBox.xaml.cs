using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MobileBox.xaml
    /// </summary>
    public partial class MobileBox : Window
    {
        string OldText;
        int BackNotPressed = 1;
        string delimiter="-";
        public MobileBox()
        {
            InitializeComponent();
            OldText = "";
        }

        private void mobileTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox=sender as TextBox;
            string newText=textbox.Text;
            int caretIndex =textbox.CaretIndex;

            string buffer = dettach(newText);
            buffer = attach(buffer);

            mobileTextBox.Text = buffer;
            setCaretIndex(sender,caretIndex);
            OldText= buffer;
        }

        public void setCaretIndex(object sender,int index) {
            var textbox = sender as TextBox;
            if (index==5||index==9||index==12)
            {
                textbox.Select(index+BackNotPressed, 0);
            }
            else
            {
                textbox.Select(index, 0);
            }
        }

        /// <summary>
        /// normalize phone number
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public string attach(string inputText)
        {
           
            int i1 = 4, i2 = 8, i3 = 11;
            int length = inputText.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == i1||i==i2||i==i3)
                {
                    inputText = inputText.Insert(i, delimiter);                   
                    length++;
                }
            }
            return inputText;
        }

        /// <summary>
        /// convert to raw number
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public string dettach(string inputText) 
        {
            string text = string.Empty;
            for (int i = 0; i < inputText.Length; i++)
            {
                char c= inputText[i];
                if (c != delimiter[0])
                {
                    text += c;
                }
            }
            return text;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mobileTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {            
            string text = e.Text;
            Regex regex = new Regex(@"^\d+");
            e.Handled= !regex.IsMatch(text);
        }
        
        private void mobileTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Space)
            {                
                e.Handled = true;
            }

            if (e.Key == Key.Back)
            {
                BackNotPressed = 0;
            }
            else if (e.Key == Key.Delete)
            {
                BackNotPressed = 0;
            }
            else
            {
                BackNotPressed = 1;
            }
        }
    }
}
