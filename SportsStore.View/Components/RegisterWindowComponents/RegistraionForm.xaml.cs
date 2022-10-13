using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SportsStore.View.Components.RegisterWindowComponents
{
    public partial class RegistraionForm : UserControl
    {
        public static RegistraionForm Instance { get; private set; }
        public RegistraionForm()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
