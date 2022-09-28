using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SportsStore.View.Utilities
{
    public class CmboBoxFiller
    {
        public  static void Fill(Enum sender, ComboBox box)
        {
            foreach (var item in Enum.GetValues(sender.GetType()))
            {
                box.Items.Add(item);
            }
        }
    }
}
