using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using SportsStore.Controller;

namespace SportsStore.View.Themes.CustomControls.LogsTabComponents
{
    public partial class LogsInfo : UserControl
    {
        public static LogsInfo? Current { get; set; }
        public LogsInfo()
        {
            InitializeComponent();
            Current = this;
        }
        
        private void BtnClearLogs_Click(object sender, RoutedEventArgs e)
        {
            LogEntityHandler handler = new();
            System.Windows.Forms.SaveFileDialog saveFileDialog = new();
            saveFileDialog.ShowDialog();
            List<string> list = new();
            foreach (var item in handler.GetTable("Logs").ToList())
            {
                list.Add(item.ToString());
            }
            File.WriteAllLines(saveFileDialog.FileName, list);

            handler.Clear();
        }

        private void BtnRefreshView_Click(object sender, RoutedEventArgs e)
        {
            TboxInfo.Text = string.Empty;
        }
    }
}
