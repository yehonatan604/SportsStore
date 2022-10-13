using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls.UsersTabComponents
{
    public partial class UsersInfo : UserControl
    {
        public static UsersInfo? Current { get; private set; }
        public UsersInfo()
        {
            InitializeComponent();
            Current = this;
        }
    }
}
