using FirstFloor.ModernUI.Windows.Controls;
using th.AdminibotModern.Properties;
using th.AdminibotModern.ViewModels;
using th.AdminibotModern.Views.Settings.AdminibotSettings;

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
