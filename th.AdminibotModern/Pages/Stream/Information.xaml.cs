using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace th.AdminibotModern.Pages
{
    /// <summary>
    /// Interaction logic for StreamInfo.xaml
    /// </summary>
    public partial class StreamInfo : UserControl
    {
        public class DataObject
        {
            public string streamInformationTitle { get; set; }
            public string streamInformationContent { get; set; }
        }
        
        public StreamInfo()
        {
            InitializeComponent();

            var list = new ObservableCollection<DataObject>();
            list.Add(new DataObject() { streamInformationTitle = "Online", streamInformationContent = "False" });
            list.Add(new DataObject() { streamInformationTitle = "Title", streamInformationContent = "Punday Monday: Playing Random Indie Games" });
            list.Add(new DataObject() { streamInformationTitle = "Game", streamInformationContent = "Holy Potatoes! A Weapon Shop?!" });
            this.streamInformationGrid.ItemsSource = list;
        }
    }
}
