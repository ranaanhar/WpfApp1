﻿using System;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = null;
            DependencyObject currentPage = VisualTreeHelper.GetParent(this);
            while (currentPage != null && frame == null)
            {
                frame = currentPage as Frame;
                currentPage = VisualTreeHelper.GetParent(currentPage);
            }


            frame.Source = new Uri("Page1.xaml", UriKind.Relative);
        }
    }
}
