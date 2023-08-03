using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserPhone.xaml
    /// </summary>
    public partial class UserPhone : UserControl
    {
       
        int BackNotPressed = 1;
        string delimiter = "-";
        public UserPhone()
        {
            InitializeComponent();
            clearElement();
           
        }
        public static readonly DependencyProperty patternProperty = DependencyProperty.Register("pattern", typeof(string), typeof(UserPhone));
        public static readonly DependencyProperty inputPatternProperty = DependencyProperty.Register("inputPattern", typeof(string), typeof(UserPhone));
        public static readonly DependencyProperty titleProperty = DependencyProperty.Register("title", typeof(string), typeof(UserPhone));
        public static readonly DependencyProperty errorMessgaeProperty = DependencyProperty.Register("errorMessage", typeof(string), typeof(UserPhone));


        public string pattern
        {
            get { return (string)GetValue(patternProperty); }
            set { SetValue(patternProperty, value); }
        }
        public string inputPattern
        {
            get { return (string)GetValue(inputPatternProperty); }
            set { SetValue(inputPatternProperty, value); }
        }

        public string title
        {
            get { return (string)GetValue(titleProperty); }
            set { SetValue(titleProperty, value); }

        }
        public string errorMessage
        {
            get { return (string)GetValue(errorMessgaeProperty); }
            set { SetValue(errorMessgaeProperty, value); }
        }
        public bool IsMatch { get; set; }

        public string Text { get { return getText(); } set { setText(value); } }

        public void clearElement() {
            mTextbox.Text = "";
        }

       

        private void mTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            mTextBox_Validate();
        }
        void mTextBox_Validate()
        {
            if (!string.IsNullOrEmpty(this.getText()) &&
                    !Validation.Validator.IsMatch(this.pattern, this.getText()))
            {
                mtextblock.Text = this.errorMessage;
                mtextblock.Foreground = Brushes.Red;
            }
            else
            {
                mtextblock.Text = string.Empty;
                mtextblock.Foreground = Brushes.Black;
            }
        }
       
        private void mTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mTextBox_Validate();
            
            var textbox = sender as TextBox;
            string newText = textbox.Text;
            int caretIndex = textbox.CaretIndex;

            string buffer = dettach(newText);
            buffer = attach(buffer);

            mTextbox.Text = buffer;
            setCaretIndex(sender, caretIndex);
        }

        string getText() {
            string text = mTextbox.Text;
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit( text[i]))
                {
                    result += text[i];
                }
            }
            return result;
        }

        void setText(string text) {
            if (Validation.Validator.IsMatch(this.pattern,text))
            {
                text = attach(text);//text.Insert(4, "-").Insert(8, "-").Insert(11, "-");
                mTextbox.Text = text;
            }
        }

        private void mTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = e.Text;
            Regex regex = new Regex(@"^\d+");
            e.Handled = !regex.IsMatch(text);

        }

        private void mTextbox_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command==ApplicationCommands.Paste)
            {
                e.Handled = true;
                onPaste();
            }
            if (e.Command==ApplicationCommands.Copy)
            {
                e.Handled = true;
                onCopy();
            }
        }

        void onPaste() {
            string text = Clipboard.GetText(TextDataFormat.Text);
            if (Validation.Validator.IsMatch(this.pattern,text))
            {
                setText(text);
            }
        }

        void onCopy() {
            Clipboard.SetText(getText());
        }

        private void mTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
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

        public void setCaretIndex(object sender, int index)
        {
            var textbox = sender as TextBox;
            if (index == 5 || index == 9 || index == 12)
            {
                textbox.Select(index + BackNotPressed, 0);
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
                if (i == i1 || i == i2 || i == i3)
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
                char c = inputText[i];
                if (c != delimiter[0])
                {
                    text += c;
                }
            }
            return text;
        }

    }
}
