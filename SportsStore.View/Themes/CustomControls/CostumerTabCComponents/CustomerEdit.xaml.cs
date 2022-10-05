using SportsStore.Controller;
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

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    /// <summary>
    /// Interaction logic for CustomerEdit.xaml
    /// </summary>
    public partial class CustomerEdit : UserControl
    {
        public static CustomerEdit? Instance { get; set; }
        public Write writer;
        public Read reader;
        public CustomerEdit()
        {
            InitializeComponent();
            FillCustomersBox();

            writer = new();
            reader = new();
            Instance = this;
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;

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
                                    tb.Text != reader.GetList("CustomerDetails", customerId)[i] &&
                                    MessageBox.Show($"You are about to change Customer {tb.Name[3..]} " +
                                                    $"to: {tb.Text}.\n " +
                                                    "are you sure?",
                                                    $"Change Customer {tb.Name[3..]}",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    writer.ChangeCustomerFirstName(Convert.ToInt16(customerId), BoxFirstName.Text);
                                    i++;
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
                MainWindow.Instance.RefreshDgrid.Invoke(MainWindow.Instance.Dgrid3);
            }
        }

        private void FillCustomersBox()
        {
            foreach (string costumer in new Read().GetList("ByCostumer"))
            {
                BoxCustomerId.Items.Add(costumer);
            }
        }

        private void BoxCustomerId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoxCustomerId.SelectedItem is string s)
            {
                BoxFirstName.Text = reader.GetList("CustomerDetails", s)[0];
                BoxLastName.Text = reader.GetList("CustomerDetails", s)[1];
                BoxEmail.Text = reader.GetList("CustomerDetails", s)[2];
                BoxDateOfBirth.Text = reader.GetList("CustomerDetails", s)[3];
            }
        }
    }
}
