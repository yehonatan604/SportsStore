using SportsStore.Controller;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    public partial class CustomersViews : UserControl
    {
        private readonly Read read = MainWindow.Current.read;
        public CustomersViews()
        {
            InitializeComponent();
            read.GetList("ByDate").ForEach(x => CmbBoxByDate.Items.Add(x));
        }

        private void Tbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox Tbox)
            {
                Tbox.Text = string.Empty;
            }
        }
        private void Tbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox Tbox)
            {
                Tbox.Text = string.IsNullOrEmpty(Tbox.Text) ? 
                Tbox.Name.Contains("Max") ? "Max" : "Min" : 
                Tbox.Text;
            }
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dGrid = MainWindow.Current.Dgrid3;

            // check what is the search requirements
            bool byId = !string.IsNullOrEmpty(BoxById.Text);
            bool byDate = !string.IsNullOrEmpty(CmbBoxByDate.Text);

            // if no price was given, search will be performed on values between min(0) & max(999,999)
            string minPrice = TboxMin.Text == "Min" || TboxMin.Text == string.Empty ? "0" : TboxMin.Text;
            string maxPrice = TboxMax.Text == "Max" || TboxMax.Text == string.Empty ? 999999.ToString() : TboxMax.Text;
            string minAge = TboxAgeMin.Text == "Min" || TboxAgeMin.Text == string.Empty ? "13" : TboxAgeMin.Text;
            string maxAge = TboxAgeMax.Text == "Max" || TboxAgeMax.Text == string.Empty ? 120.ToString() : TboxAgeMax.Text;
            string minSalesCount = TboxCountMin.Text == "Min" || TboxCountMin.Text == string.Empty ? "0" : TboxCountMin.Text;
            string maxSalesCount = TboxCountMax.Text == "Max" || TboxCountMax.Text == string.Empty ? 999999.ToString() : TboxCountMax.Text;

            dGrid.ItemsSource = byId ? read.GetCustomers
                                ("ById", minAge, maxAge, minPrice, maxPrice, minSalesCount, maxSalesCount, BoxById.Text).ToList() :
                                byDate ? read.GetCustomers
                                ("ByDate", minAge, maxAge, minPrice, maxPrice, minSalesCount, maxSalesCount, CmbBoxByDate.Text).ToList() :
                                byId && byDate ? read.GetCustomers
                                ("ByDate", minAge, maxAge, minPrice, maxPrice, minSalesCount, maxSalesCount, BoxById.Text, CmbBoxByDate.Text).ToList() :
                                // else:
                                read.GetCustomers
                                (minAge, maxAge, minPrice, maxPrice, minSalesCount, maxSalesCount).ToList();
        }

        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid3.ItemsSource = read.CustomerSearch(BoxSearch.Text).ToList();
        }
    }
}
