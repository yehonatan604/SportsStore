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
using SportsStore.View.Themes.CustomControls.CostumerTabCComponents;
using SportsStore.View.Themes.CustomControls.UsersTabComponents;
using SportsStore.View.Themes.CustomControls.LogsTabComponents;

namespace SportsStore.View
{
    public delegate void RefreshDgrid(DataGrid? dGrid = null);

    public partial class MainWindow : Window
    {
        // Fields
        private readonly Create writer;
        private readonly Read reader;
        public RefreshDgrid RefreshDgrid { get; set; }
        public static MainWindow Instance { get; private set; }
        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            reader = new();
            writer = new();
            writer.OnStartProgram();
            
            if (!Create.IsRememberMe)
            {
                LogInWindow loginWindow = new();
                loginWindow.ShowDialog();
            }

            RefreshDgrid = new(RefreshDataGrid);
            RefreshDgrid.Invoke();
            DgridController(Dgrid1);

            FillBoxes();
            StartClock();
        }

        // Fill ComboBoxes

        private void FillBoxes()
        {
            CmboBoxFiller.Fill(new ItemTypes(), StockViews.Instance.CmbBoxViewByItemType);
            CmboBoxFiller.Fill(new ItemTypes(), EditStockAddItem.Instance.BoxItemType);
            CmboBoxFiller.Fill(new ItemTypes(), EditStockEditItem.Instance.BoxItemType);
            CmboBoxFiller.Fill(new ItemTypes(), SaleViews.Instance.CmbBoxByItemType);
            CmboBoxFiller.Fill(new ColorTypes(), EditStockAddItem.Instance.BoxColor);
            CmboBoxFiller.Fill(new ColorTypes(), EditStockEditItem.Instance.BoxColor);
            CmboBoxFiller.FillSalesBoxes();
            CmboBoxFiller.FillUserBoxes();
            CmboBoxFiller.FillLogsBoxes();
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
        private void DGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        // Customers_Tab Event Handlers
        private void Customers_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RefreshDataGrid(Dgrid3);
            DgridController(Dgrid3);
            CustomerStats.Instance.CollectCharts();
        }
        private void Dgrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CustomersInfo.Instance.TboxInfo.Text = $"Customer: {GetDgridContent(Dgrid3, 1)} {GetDgridContent(Dgrid3, 2)}\n" +
                                    $"id: {GetDgridContent(Dgrid3)}\n" +
                                    $"Email: {GetDgridContent(Dgrid3, 3)}\n\n" +
                                    $"Purchases Count: {GetDgridContent(Dgrid3, 7)}\n" +
                                    $"Total Purchases: {float.Parse(GetDgridContent(Dgrid3, 6))}$\n" +
                                    $"Last Purchase at: {GetDgridContent(Dgrid3, 9)}\n";
            }
            catch
            {
                RefreshDataGrid(Dgrid3);
            }
        }

        // EditStock_Tab Event Handlers
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
            RefreshDataGrid(Dgrid2);
            DgridController(Dgrid2);
            SalesStats.Instance.CollectCharts();
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
            RefreshDataGrid(Dgrid4);
            DgridController(Dgrid4);
        }
        private void Dgrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (GetDgridContent(Dgrid4) != "")
                {
                    UsersInfo.Instance.TboxInfo.Text =
                        $"{GetDgridContent(Dgrid4, 1)} {GetDgridContent(Dgrid4, 2)}\n" +
                        $"id: {GetDgridContent(Dgrid4)}\n" +
                        $"User Type: {GetDgridContent(Dgrid4, 4)}\n" +
                        $"Email: {GetDgridContent(Dgrid4, 5)}\n" +
                        $"Hire Date: {GetDgridContent(Dgrid4, 3)}\n\n" +
                        $"Last Sale: {GetDgridContent(Dgrid4, 8)}\n" +
                        $"Sales Total: {GetDgridContent(Dgrid4, 9)}\n" +
                        $"Sales Count: {GetDgridContent(Dgrid4, 10)}";

                    UsersEdit.Instatnce.BoxUserId.Text = GetDgridContent(Dgrid4);
                }
            }
            catch
            {
                RefreshDataGrid(Dgrid4);
            }
        }

        // Logs_Tab Event Handlers
        private void Logs_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RefreshDataGrid(Dgrid5);
            DgridController(Dgrid5);
        }
        private void Dgrid5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (GetDgridContent(Dgrid5) != "")
                {
                    LogsInfo.Instance.TboxInfo.Text =
                        $"{GetDgridContent(Dgrid5, 1)} {GetDgridContent(Dgrid5, 2)}\n" +
                        $"id: {GetDgridContent(Dgrid5)}\n" +
                        $"User Type: {GetDgridContent(Dgrid5, 4)}\n" +
                        $"Email: {GetDgridContent(Dgrid5, 5)}\n" +
                        $"Hire Date: {GetDgridContent(Dgrid5, 3)}\n\n";
                }
            }
            catch
            {
                RefreshDataGrid(Dgrid5);
            }
        }

        // OnExit Event Handler
        private void OnExit(object sender, CancelEventArgs e)
        {
            writer.OnExitProgram();
        }

        // ComboBox Fillers

        // DataGrids Handlers
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
                Dgrid3.ItemsSource = reader.GetTable("Customers").ToList();
            }
            if (dGrid == Dgrid4)
            {
                Dgrid4.ItemsSource = reader.GetTable("Users").ToList();
            }
            if (dGrid == Dgrid5)
            {
                Dgrid5.ItemsSource = reader.GetTable("Logs").ToList();
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

