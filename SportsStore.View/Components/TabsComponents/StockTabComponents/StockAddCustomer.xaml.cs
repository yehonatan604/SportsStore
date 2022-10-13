using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockAddCustomer : UserControl
    {
        public StockAddCustomer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerEntityHandler handler = new();
            handler.Add(TBoxAddFname.Text, TBoxAddLname.Text, TBoxAddEmail.Text, DPickerAddDOB.SelectedDate.ToString());
        }
    }
}
