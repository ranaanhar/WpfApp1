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
    /// Interaction logic for UserTextbox.xaml
    /// </summary>
    public partial class UserTextbox : UserControl
    {
        public UserTextbox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty patternProperty = DependencyProperty.Register("pattern", typeof(string), typeof(UserTextbox));
        public static readonly DependencyProperty inputPatternProperty = DependencyProperty.Register("inputPattern", typeof(string), typeof(UserTextbox));
        public static readonly DependencyProperty titleProperty = DependencyProperty.Register("title", typeof(string), typeof(UserTextbox));
        public static readonly DependencyProperty errorMessgaeProperty = DependencyProperty.Register("errorMessage", typeof(string), typeof(UserTextbox));
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
                mValidation();
            }
        }

        private void mTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox && !string.IsNullOrEmpty(this.pattern))
            {
                mValidation();
            }        
        }

        private void mValidation() {
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
            catch (Exception )
            {
                //some code
            }
        }

        public void clearElement() {
            mTextbox.Clear();
        }

        private void userTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            mTextbox.Focus();
        }
    }
}
