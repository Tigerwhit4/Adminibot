﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

namespace th.AdminibotModern.Pages.Settings.AdminibotSettings
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

        // 9 accent colors from metro design principles
        private Color[] _accentColors = new Color[]{
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x33, 0x99, 0x33),   // green
            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };

        // 20 accent colors from Windows Phone 8
        /*private Color[] accentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };*/

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
