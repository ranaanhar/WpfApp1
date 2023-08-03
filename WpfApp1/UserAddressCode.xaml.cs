using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for UserAddressCode.xaml
    /// </summary>
    public partial class UserAddressCode : UserControl
    {
        private const int LENGTH_1 = 2, LENGTH_2 = 4;
        public UserAddressCode()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty patternProperty = DependencyProperty.Register("pattern", typeof(string), typeof(UserAddressCode));
        public static readonly DependencyProperty inputPatternProperty = DependencyProperty.Register("inputPattern", typeof(string), typeof(UserAddressCode));
        public static readonly DependencyProperty titleProperty = DependencyProperty.Register("title", typeof(string), typeof(UserAddressCode));
        public static readonly DependencyProperty errorMessgaeProperty = DependencyProperty.Register("errorMessage", typeof(string), typeof(UserAddressCode));
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

        public string Text { get { return mTextbox.Text; } set { mTextbox.Text = value; } }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox && !string.IsNullOrEmpty(this.inputPattern))
            {
                Regex regex = new Regex(this.inputPattern);
                e.Handled = !regex.IsMatch(e.Text);
            }
        }

        private void mTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox && !string.IsNullOrEmpty(this.pattern))
            {
                string text = mTextbox.Text;
                //if (string.IsNullOrEmpty(text))
                //    return;
                string[] stringArray = text.Split('.');
                int length = stringArray.Length;
                if (length == 3 && stringArray[2].Length > 0)
                {
                    normalizeAddressTextbox(6, LENGTH_2 - stringArray[2].Length);
                }
                if (length==4)
                {
                    normalizeAddressTextbox(11, LENGTH_2 - stringArray[3].Length);
                }
                //mValidation();
            }
        }

        private void mTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox && !string.IsNullOrEmpty(this.pattern))
            {
                if (lengthCheck()){return;}
                processAddressTextBox();
                mValidation();
            }
        }

        void processAddressTextBox() {
            string text = mTextbox.Text;
            if (text.Contains('.'))
            {
                string[] stringArray = text.Split('.');
                int length = stringArray.Length;

                if (length <= 4)
                {
                    if (!text.EndsWith('.'))
                    {
                        if (text.Length == 6 || text.Length == 11)
                        {
                            normalizeAddressTextbox(text.Length - 1, ".");
                        }
                    }
                    else
                    {
                        if (length == 2 && stringArray[0].Length == 1)
                        {
                            normalizeAddressTextbox(0, LENGTH_1 - stringArray[0].Length);
                        }
                        if (length == 3 && stringArray[1].Length == 1)
                        {
                            normalizeAddressTextbox(3, LENGTH_1 - stringArray[1].Length);
                        }
                        if (length == 4 && stringArray[2].Length < 4 && stringArray[2].Length > 0)
                        {
                            normalizeAddressTextbox(6, LENGTH_2 - stringArray[2].Length);
                        }
                    }
                }
            }
            else if (text.Length == 3)
            {
                normalizeAddressTextbox(text.Length - 1, ".");
            }
        }
        bool lengthCheck() {
            string text = mTextbox.Text;
            if (text.Length > 15)
            {
                text = text.Substring(0, 15);
                mTextbox.Text = text;
                mTextbox.Select(text.Length, 0);
                return true;
            }
            return false;
        }

        private void normalizeAddressTextbox(int index, int length)
        {
            if (length < 0)
                return;
            string text = mTextbox.Text;
            for (int i = 0; i < length; i++)
            {
                text = text.Insert(index, "0");
            }

            mTextbox.Text = text;
            mTextbox.Select(text.Length, 0);
        }
        private void normalizeAddressTextbox(int index, string character)
        {
            string text = mTextbox.Text;
            text = text.Insert(index, character);
            mTextbox.Text = text;
            mTextbox.Select(text.Length, 0);
        }

        private void mValidation()
        {
            try
            {
                string text = mTextbox.Text;
                IsMatch = Validation.Validator.IsMatch(pattern, text);
                if (!IsMatch && !string.IsNullOrEmpty(text))
                {
                    mtextBlock.Foreground = Brushes.Red;
                    mtextBlock.Text = errorMessage;
                }
                else
                {
                    mtextBlock.Foreground = Brushes.Black;
                    mtextBlock.Text = title;
                }
            }
            catch{}
        }

        public void clearElement() {
            mTextbox.Clear();
        }
    }
}
