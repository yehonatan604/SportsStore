using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockAddCustomer : UserControl
    {
        private readonly Create write = MainWindow.Current.write;
        public StockAddCustomer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            write.AddCustomer(TBoxAddFname.Text, TBoxAddLname.Text, TBoxAddEmail.Text, Convert.ToDateTime(DPickerAddDOB.SelectedDate));
        }
    }
}
