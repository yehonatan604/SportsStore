using SportsStore.Controller;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SportsStore.View.Themes.CustomControls.LogsTabComponents
{
    /// <summary>
    /// Interaction logic for LogsInfo.xaml
    /// </summary>
    public partial class LogsInfo : UserControl
    {
        public static LogsInfo Instance;
        private readonly Delete deleter;
        public LogsInfo()
        {
            InitializeComponent();
            Instance = this;
            deleter = new();
        }
        
        private void BtnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new();
            saveFileDialog.ShowDialog();
            List<string> list = new();
            foreach (var item in MainWindow.Instance.Dgrid5.Items)
            {
                list.Add(item.ToString());
            }
            File.WriteAllLines(saveFileDialog.FileName, list);
        }

        private void BtnClearLogs_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new();
            saveFileDialog.ShowDialog();
            List<string> list = new();
            foreach (var item in new Read().GetTable("Logs").ToList())
            {
                list.Add(item.ToString());
            }
            File.WriteAllLines(saveFileDialog.FileName, list);

            deleter.ClearLogs();
        }

        private void BtnRefreshView_Click(object sender, RoutedEventArgs e)
        {
            TboxInfo.Text = string.Empty;
        }
    }
}
