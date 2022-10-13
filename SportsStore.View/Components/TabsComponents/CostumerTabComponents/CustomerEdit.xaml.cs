using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    public partial class CustomerEdit : UserControl
    {
        public static CustomerEdit? Current { get; private set; }
        private readonly CustomerEntityHandler handler;
        public CustomerEdit()
        {
            handler = new();
            InitializeComponent();
            FillCustomersBox();
            Current = this;
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BoxCustomerId.SelectedItem is string customerId)
                {
                    foreach (Grid grid in OuterWrapPanel.Children)
                    {
                        foreach (FrameworkElement control in grid.Children)
                        {
                            if (control is TextBox tb)
                            {
                                if (!string.IsNullOrEmpty(tb.Text) &&
                                    MessageBox.Show($"You are about to change Customer {tb.Name[3..]} " +
                                                    $"to: {tb.Text}.\n " +
                                                    "are you sure?",
                                                    $"Change Customer {tb.Name[3..]}",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    switch (tb.Name[3..])
                                    {
                                        case "FirstName":
                                            {
                                                handler.Update("FirstName", new string[] { customerId, tb.Text });
                                                break;
                                            };
                                        case "LastName": 
                                            {
                                                handler.Update("LastName", new string[] { customerId, tb.Text }); 
                                                break; 
                                            };
                                        case "Email": 
                                            { 
                                                handler.Update("Email", new string[] { customerId, tb.Text }); 
                                                break; 
                                            };
                                        case "DOB": 
                                            { 
                                                handler.Update("DOB", new string[] { customerId, tb.Text }); 
                                                break; 
                                            };
                                    };
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Customer's Details was changed successfuly");
            }
            catch
            {
                MessageBox.Show("Customer Change Details operation failed!!!");
            }
            finally
            {
                DGridController.RefreshDgrid(MainWindow.Current.Dgrid3);
            }
        }

        private void FillCustomersBox()
        {
            foreach (string costumer in handler.GetList("ByCustomer"))
            {
                BoxCustomerId.Items.Add(costumer);
            }
        }

        private void BoxCustomerId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoxCustomerId.SelectedItem is string s)
            {
                BoxFirstName.Text = handler.GetList("CustomerDetails", s)[0];
                BoxLastName.Text = handler.GetList("CustomerDetails", s)[1];
                BoxEmail.Text = handler.GetList("CustomerDetails", s)[2];
                BoxDateOfBirth.Text = handler.GetList("CustomerDetails", s)[3];
            }
        }
    }
}
