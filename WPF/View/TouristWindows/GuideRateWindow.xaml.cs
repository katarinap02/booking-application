using BookingApp.Domain.Model;
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
using Microsoft.Win32;
using System.Globalization;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for GuideRateWindow.xaml
    /// </summary>
    public partial class GuideRateWindow : Window
    {
        public GuideRateViewModel GuideRate { get; set; }

        public GuideRateWindow(TourViewModel selectedTour, int userId)
        {
            InitializeComponent();
            GuideRate = new GuideRateViewModel();
            DataContext = GuideRate;

            GuideRate.UserId = userId;
            if (GuideRate.initializeGuideRate(selectedTour.Id, selectedTour.GuideId))
                Close();

        }

        private void Addimage_Click(object sender, RoutedEventArgs e)
        {
            GuideRate.AddImage();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            GuideRate.Submit(GuideRate);
            Close();
        }

        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(CommentTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PlaceholderTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CommentTextBox.Focus();
        }
    }
}
