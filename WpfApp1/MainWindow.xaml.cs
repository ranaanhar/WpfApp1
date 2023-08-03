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
using System.Xml.Linq;
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDatabase db;
        DatabaseHandler.MoshtarakDbHandler moshtarakDbHandler;
        Validation.Validator mValidator;
        int pages = 1;
        int currentPage = 1;
        Moshtarak lastSearchedMoshtarak;
        private int limit = 100;



        public int Pages { get { return pages; } set { pages = (value > 0) ? value : 1; } }
        public int CurrentPage { get { return currentPage; } set { currentPage = (value > 0) ? value : 1; } }



        public MainWindow()
        {
            InitializeComponent();
            db = new MyDatabase();
            moshtarakDbHandler = new DatabaseHandler.MoshtarakDbHandler(db);
            mValidator = new Validation.Validator();

        }

        void setFocus()
        {
            try
            {
                FocusManager.SetFocusedElement(this, userEshterak);
            }
            catch { }
        }
       
        
        bool ValidateInputBeforeSave(Moshtarak moshtarak) {

            if (mValidator.EshterakValidtaion(moshtarak.Eshterak) && mValidator.AddressCodeValidation(moshtarak.AddressCode) &&
                mValidator.phoneValidation(moshtarak.Tel1) && mValidator.phoneValidation(moshtarak.Tel2) &&
                stringValidate(moshtarak.Name) && stringValidate(moshtarak.Family) && stringValidate(moshtarak.Label1) && stringValidate(moshtarak.Label2))
            {
                return true;
            }
            return false;
        }
      
        
        void save()
        {
            string resultMessage = "";          
            Moshtarak moshtarak = GetMoshtarakFromUI();

            if (ValidateInputBeforeSave(moshtarak))
            {
                try
                {
                    
                    int resultCode = moshtarakDbHandler.Insert(moshtarak);

                    if (resultCode == 1)
                        resultMessage = moshtarak + " inserted \n";

                    else if (resultCode == 0)
                    {
                        resultCode = moshtarakDbHandler.Update(moshtarak);
                        if (resultCode == 1)
                        {
                            resultMessage += moshtarak + " updated\n";
                        }
                    }
                    else
                    {
                        resultMessage = "result=" + resultCode;
                    }
                    clearElement();

                }
                catch (Exception exp)
                {
                    resultMessage = exp.Message;
                }

            }
            else
            {
                resultMessage = "dadeha ra sahih vared konid";
            }
            label.Content = resultMessage;
        }

        void clearElement()
        {
            userEshterak.clearElement();
            userAddressCode.clearElement();
            userPhone1.clearElement();
            userPhone2.clearElement();
            userName.clearElement();
            userFamily.clearElement();
            userTozihat1.clearElement();
            userTozihat2.clearElement();
            setFocus();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                save();
            }
            catch (Exception exp)
            {
                label.Content = exp.Message;
            }
        }

        private bool stringValidate(string inputText)
        {
            List<string> keyWord = new List<string>() { "select", "where", "from", "update", "insert", "delete", "join", "exist", "like" };

            foreach (string item in keyWord)
            {
                if (inputText.ToLower().Contains(item))
                {
                    return false;
                }
            }
            return true;
        }



        private void eshterakUserTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = "";
            if (sender is UserTextbox)
            {
                text = (sender as UserTextbox).mTextbox.Text;
            }
            if (mValidator.EshterakValidtaion(text))
            {
                SendDataToSearch();
            }
        }



        void fillControls(Model.Moshtarak moshtarak)
        {
            userEshterak.Text = moshtarak.Eshterak;
            userAddressCode.Text = moshtarak.AddressCode;
            userName.Text = moshtarak.Name;
            userFamily.Text = moshtarak.Family;
            userPhone1.Text = moshtarak.Tel1;
            userPhone2.Text = moshtarak.Tel2;
            userTozihat1.Text = moshtarak.Label1;
            userTozihat2.Text = moshtarak.Label2;
        }



        void search(Moshtarak moshtarak)
        {
            //save last search keyword
            lastSearchedMoshtarak = moshtarak;

            List<Moshtarak> result;
            int count;
            int offset = currentPage - 1;

            //search from dataBase
            result = moshtarakDbHandler.Search(moshtarak, limit, offset, out count);
            Pages = (int)Math.Ceiling(count / (double)limit);

            //set pageLabel to current page
            pageLabel.Content = CurrentPage + " of " + Pages;

            //set dataGrid data
            setDataGrid(result);

            if (result.Count == 1)
                fillControls(result[0]);
        }

        void setDataGrid(List<Model.Moshtarak> result)
        {
            dataGrid.ItemsSource = result;
            dataGrid.Items.SortDescriptions.Clear();
            dataGrid.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("AddressCode",
                System.ComponentModel.ListSortDirection.Ascending));
            dataGrid.Items.Refresh();
            dataGrid.ScrollIntoView(dataGrid.Items[0]);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SendDataToSearch();
        }

        private void SendDataToSearch()
        {
            CurrentPage = 1;
            Moshtarak moshtarak = GetMoshtarakFromUI();
            if (moshtarak != null)
                search(moshtarak);
        }

        private Moshtarak GetMoshtarakFromUI()
        {
            try
            {
                var moshtarak = new Moshtarak()
                {
                    Eshterak = userEshterak.Text,
                    AddressCode = userAddressCode.Text,
                    Name = userName.Text,
                    Family = userFamily.Text,
                    Tel1 = userPhone1.Text,
                    Tel2 = userPhone2.Text,
                    Label1 = userTozihat1.Text,
                    Label2 = userTozihat2.Text
                };

                return moshtarak;
            }catch { return null; }
        }

        private void clear_click(object sender, RoutedEventArgs e)
        {
            clearElement();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setFocus();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < this.Pages)
            {
                CurrentPage++;
                search(lastSearchedMoshtarak);
            }
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                search(lastSearchedMoshtarak);
            }
        }
    }
}
