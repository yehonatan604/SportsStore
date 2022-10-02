using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Utilities;
using System.Windows.Media;
using SportsStore.View.Themes.CustomControls;
using SportsStore.View.Themes.CustomControls.EditStockTabComponents;
using SportsStore.View.Themes.CustomControls.SalesTabComponents;

namespace SportsStore.View
{
    public delegate void RefreshDgrid(DataGrid? dGrid = null);

    public partial class MainWindow : Window
    {
        // Fields
        private readonly Write writer;
        private readonly Read reader;
        public RefreshDgrid RefreshDgrid { get; set; }
        public static MainWindow Instance { get; private set; }
        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            writer = new();
            writer.OnStartProgram();
            if (!Write.IsRememberMe)
            {
                LogInWindow loginWindow = new();
                loginWindow.ShowDialog();
            }
            reader = new();

            CmboBoxFiller.Fill(new ItemTypes(), StockViews.Instance.CmbBoxViewByItemType);
            CmboBoxFiller.Fill(new ItemTypes(), EditStockAddItem.Instance.BoxItemType);
            CmboBoxFiller.Fill(new ItemTypes(), EditStockEditItem.Instance.BoxItemType);
            CmboBoxFiller.Fill(new ItemTypes(), SaleViews.Instance.CmbBoxByItemType);
            CmboBoxFiller.Fill(new ColorTypes(), EditStockAddItem.Instance.BoxColor);
            CmboBoxFiller.Fill(new ColorTypes(), EditStockEditItem.Instance.BoxColor);
            CmboBoxFiller.Fill(new UserTypes(), CmboBoxChangeType);
            CmboBoxFiller.FillSalesBoxes();
            FillLogsBoxes();

            RefreshDgrid = new(RefreshDataGrid);
            RefreshDgrid.Invoke();
            DgridController(Dgrid1);

            StartClock();
        }

        // Digital Clock
        private void StartClock()
        {
            DispatcherTimer clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clock.Tick += Tick;
            clock.Start();
        }
        private void Tick(object? sender, EventArgs e)
        {
            LblTime.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        // Stock_Tab Event Handlers
        private void Stocks_Tabs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RefreshDgrid.Invoke();
            DgridController(Dgrid1);
        }

        // EditStock_Tab Event Handlers
        private void BoxItemPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            Validate.IsStringNaN((TextBox)sender, e);
        }
        private void EditStock_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RefreshDgrid.Invoke();
            DgridController(Dgrid1_1);
        }
        private void DGrid1_1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dGrid)
            {
                EditStockInfo.Instance.TboxInfo.Text = $"{GetDgridContent(dGrid, 5)}\n" +
                                $"Id: {GetDgridContent(dGrid, 4)}\n" +
                                $"Price: {GetDgridContent(dGrid, 8)}\n" +
                                $"Color: {GetDgridContent(dGrid, 9)}\n" +
                                $"Size: {GetDgridContent(dGrid, 10)}\n" +
                                $"In Stock: {GetDgridContent(dGrid, 2)}\n";

                EditStockInfo.Instance.TboxInfo.Text += $"\n\n{GetDgridContent(dGrid, 5)}\n" +
                                       $"is a {GetDgridContent(dGrid, 7)} {GetDgridContent(dGrid, 6)} " +
                                       $"and it costs {GetDgridContent(dGrid, 8)} Shekels.\n" +
                                       $"we are working with this product\nsince: {GetDgridContent(dGrid, 11)}";

                EditStockAddItem.Instance.BoxId.Text = GetDgridContent(dGrid, 4);

                EditStockEditItem.Instance.BoxItemId.Text = GetDgridContent(dGrid, 4);
                EditStockEditItem.Instance.BoxItemName.Text = GetDgridContent(dGrid, 5);
                EditStockEditItem.Instance.BoxItemPrice.Text = GetDgridContent(dGrid, 8);
                EditStockEditItem.Instance.BoxQuantity.Text = GetDgridContent(dGrid, 2);
                EditStockEditItem.Instance.BoxItemType.Text = GetDgridContent(dGrid, 6);
                EditStockEditItem.Instance.BoxItemInnerType.Text = GetDgridContent(dGrid, 7);
                EditStockEditItem.Instance.BoxSize.Text = GetDgridContent(dGrid, 10);
                EditStockEditItem.Instance.BoxColor.Text = GetDgridContent(dGrid, 9);
            }
        }

        // Sales_Tab Event Handlers
        private void Sales_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (reader.CheckAuthorizationLevel() > 1)
            {
                MessageBox.Show("You are not authorized to do that!");
                return;
            }
            RefreshDataGrid(Dgrid2);
            DgridController(Dgrid2);
            SalesChart.Instance.CollectCharts();
        }
        private void Dgrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaleInfo.Instance.TboxInfo.Text = $"Sale Id: {GetDgridContent(Dgrid2)}\n\n" +
                                              $"Item: {GetDgridContent(Dgrid2, 5)}, Id: {GetDgridContent(Dgrid2, 4)}\n" +
                                              $"Type: {GetDgridContent(Dgrid2, 7)} {GetDgridContent(Dgrid2, 6)}\n\n" +
                                              $"{GetDgridContent(Dgrid2, 2)} units were soled for total of {GetDgridContent(Dgrid2, 3)}\n" +
                                              $"at {GetDgridContent(Dgrid2, 1)}\n" +
                                              $"by: {GetDgridContent(Dgrid2, 12)} {GetDgridContent(Dgrid2, 13)}, Id: {GetDgridContent(Dgrid2, 11)}";
        }
        // Users_Tab Event Handlers
        private void Users_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (reader.CheckAuthorizationLevel() != 0)
            {
                MessageBox.Show("You are not authorized to do that!");
                return;
            }
            RefreshDataGrid(Dgrid3);
            DgridController(Dgrid3);
        }
        private void Dgrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string salesCount = GetDgridContent(Dgrid3, 6) == string.Empty ? "0" : GetDgridContent(Dgrid3, 6);
                string salesTotal = GetDgridContent(Dgrid3, 5) == string.Empty ? "0" : GetDgridContent(Dgrid3, 5);

                TboxUserInfo.Text = $"User: {GetDgridContent(Dgrid3, 1)} {GetDgridContent(Dgrid3, 2)}, " +
                                    $"{reader.GetUserType(Convert.ToInt16(GetDgridContent(Dgrid3)))}\n" +
                                    $"id: {GetDgridContent(Dgrid3)}\n" +
                                    $"Email: {GetDgridContent(Dgrid3, 3)}\n\n" +
                                    $"Sales: {salesCount}\n" +
                                    $"Sales Total: {salesTotal}$\n" +
                                    $"Hire Date: {GetDgridContent(Dgrid3, 4)}\n";

                LblUserId.Text = $"User Id: {GetDgridContent(Dgrid3)}";
            }
            catch
            {
                RefreshDataGrid(Dgrid3);
            }
        }
        private void BtnChangeUserType_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will change the user type to {CmboBoxChangeType.Text}.\n " +
                                 "are you sure)?", "Change User Type",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                writer.ChangeUserType(Convert.ToInt16(GetDgridContent(Dgrid3)), CmboBoxChangeType.Text);
                RefreshDataGrid(Dgrid3);
            }

        }
        private void BtnChangeEmail_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will change user email to {TboxChangeMail.Text}.\n " +
                                 "are you sure)?", "Change User Email",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                writer.ChangeUserEmail(Convert.ToInt16(GetDgridContent(Dgrid3)), TboxChangeMail.Text);
                RefreshDataGrid(Dgrid3);
            }
        }
        private void TboxChangeMail_LostFocus(object sender, RoutedEventArgs e)
        {
            Validate.IsEmailValid(TboxChangeMail);
        }
        private void BtnChangeHireDate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will change user hire date.\n " +
                                 "are you sure)?", "Change User Hire Date",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                writer.ChangeUserHireDate(Convert.ToInt16(GetDgridContent(Dgrid3)), TboxChangeHireDate.Text);
            }
        }
        private void BtnResetPass_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will reset user password to '12345'.\n " +
                                 "are you sure)?", "Change User Password",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                writer.ChangeUserPassword(Convert.ToInt16(GetDgridContent(Dgrid3)), Md5Hash.Create("12345"));
            }
        }
        private void BtnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will remove user!.\n " +
                                 "are you sure)?", "Remove User",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                writer.RemoveUser(Convert.ToInt16(GetDgridContent(Dgrid3)));
                RefreshDataGrid(Dgrid3);
            }
        }

        // Logs_Tab Event Handlers
        private void Logs_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (reader.CheckAuthorizationLevel() != 0)
            {
                MessageBox.Show("You are not authorized to do that!");
                return;
            }
            RefreshDataGrid(Dgrid4);
            DgridController(Dgrid4);
        }
        private void Dgrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TboxLogInfo.Text = $"User: {GetDgridContent(Dgrid4, 2)} {GetDgridContent(Dgrid4, 3)}, " +
                                    $"{reader.GetUserType(Convert.ToInt16(GetDgridContent(Dgrid4, 1)))}\n" +
                                    $"id: {GetDgridContent(Dgrid4, 1)}\n\n" +
                                    $"Action Type: {GetDgridContent(Dgrid4, 4)}\n" +
                                    $"Date: {GetDgridContent(Dgrid4, 5)}\n";
            }
            catch
            {
                RefreshDataGrid(Dgrid4);
            }
        }
        private void BtnLogsGo_Click(object sender, RoutedEventArgs e)
        {
            bool byUser = CmboBoxLogByUserId.Text != string.Empty;
            bool byDate = CmboBoxLogByDate.Text != string.Empty;
            bool byAction = CmboBoxLogByAction.Text != string.Empty;

            if (byUser && byDate)
            {
                Dgrid4.ItemsSource = reader.GetTable("LogsByUserIdDate",
                                                     CmboBoxLogByDate.Text,
                                                     CmboBoxLogByUserId.Text).ToList();
            }
            if (byAction && byDate)
            {
                Dgrid4.ItemsSource = reader.GetTable("LogsByActionDate",
                                                      CmboBoxLogByAction.Text,
                                                      CmboBoxLogByDate.Text).ToList();
            }
            if (byAction && byUser)
            {
                Dgrid4.ItemsSource = reader.GetTable("LogsByActionId",
                                                      CmboBoxLogByUserId.Text,
                                                      CmboBoxLogByAction.Text).ToList();
            }
            if (byAction && byUser && byDate)
            {
                Dgrid4.ItemsSource = reader.GetTable("LogsByAll",
                                                      CmboBoxLogByDate.Text,
                                                      CmboBoxLogByUserId.Text,
                                                      CmboBoxLogByAction.Text).ToList();
            }
            return;
        }
        private void BtnMakeLogFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new();
            saveFileDialog.ShowDialog();
            List<string> list = new();
            foreach (var item in Dgrid4.Items)
            {
                list.Add(item.ToString());
            }
            File.WriteAllLines(saveFileDialog.FileName, list);
        }

        // Exit & Start Event Handlers
        private void OnExit(object sender, CancelEventArgs e)
        {
            writer.ExitProgram();
        }

        // ComboBox Fillers

        private void FillLogsBoxes()
        {
            foreach (string str in reader.GetList("ByLogUserId"))
            {
                CmboBoxLogByUserId.Items.Add(str);
            }
            foreach (string str in reader.GetList("ByLogAction"))
            {
                CmboBoxLogByAction.Items.Add(str);
            }
            foreach (string str in reader.GetList("ByLogDate"))
            {
                CmboBoxLogByDate.Items.Add(str);
            }
        }

        // DataGrids Handlers
        private void DGrid1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dGrid)
            {
                StockInfo.Instance.TboxInfo.Text = $"{GetDgridContent(dGrid, 5)}\n" +
                                $"Id: {GetDgridContent(dGrid, 4)}\n" +
                                $"Price: {GetDgridContent(dGrid, 8)}\n" +
                                $"Color: {GetDgridContent(dGrid, 9)}\n" +
                                $"Size: {GetDgridContent(dGrid, 10)}\n" +
                                $"In Stock: {GetDgridContent(dGrid, 2)}\n";

                StockInfo.Instance.TboxInfo.Text += $"\n\n{GetDgridContent(dGrid, 5)}\n" +
                                       $"is a {GetDgridContent(dGrid, 6)} {GetDgridContent(dGrid, 7)} " +
                                       $"and it costs {GetDgridContent(dGrid, 8)} Shekels.\n" +
                                       $"we are working with this product\nsince: {GetDgridContent(dGrid, 11)}";

                StockSell.Instance.TboxSellID.Text = GetDgridContent(dGrid, 4);
            }
        }
        private void DgridController(DataGrid currentGrid)
        {
            foreach (var control in StockTabGrid.Children)
            {
                if (control is DataGrid grid)
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            }
            currentGrid.Visibility = Visibility.Visible;
        }
        private void RefreshDataGrid(DataGrid? dGrid = null)
        {
            if (dGrid == null)
            {
                Dgrid1.ItemsSource = reader.GetTable().ToList();
                Dgrid1_1.ItemsSource = reader.GetTable().ToList();
            }
            if (dGrid == Dgrid2)
            {
                Dgrid2.ItemsSource = reader.GetTable("Sales").ToList();
            }
            if (dGrid == Dgrid3)
            {
                Dgrid3.ItemsSource = reader.GetTable("Users").ToList();
            }
            if (dGrid == Dgrid4)
            {
                Dgrid4.ItemsSource = reader.GetTable("Logs").ToList();
            }
        }
        private string GetDgridContent(DataGrid dGrid, int cell = 0)
        {
            try
            {
                return ((TextBlock)dGrid.SelectedCells[cell].Column.GetCellContent(dGrid.SelectedCells[cell].Item)).Text;

            }
            catch
            {
                return string.Empty;
            }
        }

        // Global Handlers for tab headers
        private void TabHdrStock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                tb.Foreground = new SolidColorBrush(Colors.Wheat);
            }
        }
        private void TabHdrStock_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                tb.Foreground = new SolidColorBrush(Colors.LemonChiffon);
            }
        }
    }
}

