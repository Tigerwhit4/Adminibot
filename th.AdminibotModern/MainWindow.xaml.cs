using FirstFloor.ModernUI.Windows.Controls;
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
using th.AdminibotModern.Pages.Settings;
using th.AdminibotModern.Properties;

namespace th.AdminibotModern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            AppearanceViewModel settings = new AppearanceViewModel();
            settings.SetThemeAndColor(Settings.Default.SelectedThemeDisplayName,
                  Settings.Default.SelectedThemeSource,
                  Settings.Default.SelectedAccentColor,
                  Settings.Default.SelectedFontSize);
        }
    }
}
