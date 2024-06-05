using BookingApp.Domain.Model.Features;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideTestWindows
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {
        public ReviewsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var tourists = new List<Tourist1>
            {
                new Tourist1 { Name = "John Doe", GuideGeneralKnowledge = "4/5", GuideLanguageKnowledge = "5/5", TourInterestLevel = "4/5", LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Checkpoint = "Checkpoint 1" },
                new Tourist1 { Name = "Jane Smith", GuideGeneralKnowledge = "3/5", GuideLanguageKnowledge = "4/5", TourInterestLevel = "5/5", LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Checkpoint = "Checkpoint 2" }
            };
            TouristListView.ItemsSource = tourists;
        }
    }

    public class Tourist1
    {
        public string Name { get; set; }
        public string GuideGeneralKnowledge { get; set; }
        public string GuideLanguageKnowledge { get; set; }
        public string TourInterestLevel { get; set; }
        public string LongDescription { get; set; }
        public string Checkpoint { get; set; }
    }
}
