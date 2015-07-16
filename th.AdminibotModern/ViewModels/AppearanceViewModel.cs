using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

namespace th.AdminibotModern.ViewModels
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class AppearanceViewModel
        : NotifyPropertyChanged
    {
        private const string FontSmall = "Small";
        private const string FontLarge = "Large";
        private bool _colorLoadedYet;

        private Color[] _accentColors = new Color[]{
            Color.FromRgb(0x33, 0x99, 0xff),
            Color.FromRgb(0x00, 0xab, 0xa9),
            Color.FromRgb(0x33, 0x99, 0x33),
            Color.FromRgb(0x8c, 0xbf, 0x26),
            Color.FromRgb(0xf0, 0x96, 0x09),
            Color.FromRgb(0xff, 0x45, 0x00),
            Color.FromRgb(0xe5, 0x14, 0x00),
            Color.FromRgb(0xff, 0x00, 0x97),
            Color.FromRgb(0xa2, 0x00, 0xff),
        };

        private Color _selectedAccentColor;
        private LinkCollection _themes = new LinkCollection();
        private Link _selectedTheme;
        private string _selectedFontSize;

        public AppearanceViewModel()
        {
            // add the default themes
            this._themes.Add(new Link { DisplayName = "Dark", Source = AppearanceManager.DarkThemeSource });
            this._themes.Add(new Link { DisplayName = "Light", Source = AppearanceManager.LightThemeSource });

            this.SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;
            SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this._themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = AppearanceManager.Current.AccentColor;

            if (this._colorLoadedYet)
            {
                Properties.Settings.Default.SelectedThemeDisplayName = this.SelectedTheme.DisplayName;
                Properties.Settings.Default.SelectedThemeSource = this.SelectedTheme.Source;
                Properties.Settings.Default.SelectedAccentColor = this.SelectedAccentColor;
                Properties.Settings.Default.SelectedFontSize = this.SelectedFontSize;
                Properties.Settings.Default.Save();
            }
        }

        public void SetThemeAndColor (string themeSourceDisplayName, Uri themeSourceUri, Color accentColor, string fontSize)
        {
            this.SelectedTheme = new Link { DisplayName = themeSourceDisplayName, Source = themeSourceUri };
            this.SelectedAccentColor = accentColor;
            this.SelectedFontSize = fontSize;
            this._colorLoadedYet = true;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }

        public LinkCollection Themes
        {
            get { return this._themes; }
        }

        public string[] FontSizes
        {
            get { return new string[] { FontSmall, FontLarge }; }
        }

        public Color[] AccentColors
        {
            get { return this._accentColors; }
        }

        public Link SelectedTheme
        {
            get { return this._selectedTheme; }
            set
            {
                if (this._selectedTheme != value)
                {
                    this._selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");

                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

        public string SelectedFontSize
        {
            get { return this._selectedFontSize; }
            set
            {
                if (this._selectedFontSize != value)
                {
                    this._selectedFontSize = value;
                    OnPropertyChanged("SelectedFontSize");

                    if (_colorLoadedYet)
                    {
                        Properties.Settings.Default.SelectedFontSize = this._selectedFontSize;
                        Properties.Settings.Default.Save();
                    }

                    AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
                }
            }
        }

        public Color SelectedAccentColor
        {
            get { return this._selectedAccentColor; }
            set
            {
                if (this._selectedAccentColor != value)
                {
                    this._selectedAccentColor = value;
                    OnPropertyChanged("SelectedAccentColor");

                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }
    }
}
