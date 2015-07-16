﻿using System.Windows.Controls;
using th.AdminibotModern.ViewModels;

namespace th.AdminibotModern.Views.Settings.AdminibotSettings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new AppearanceViewModel();
        }
    }
}
