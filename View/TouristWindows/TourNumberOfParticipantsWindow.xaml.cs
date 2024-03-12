using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourNumberOfParticipantsWindow.xaml
    /// </summary>
    public partial class TourNumberOfParticipantsWindow : Window
    {

        private Tour SelectedTour;
        private int InsertedNumberOfParticipants;
        public TourNumberOfParticipantsWindow(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;

            NumberOfParticipants.Text = "1";

            if(SelectedTour.AvailablePlaces == 0)
            {
                availablePlaces.Foreground = Brushes.Red;
            }
            else
            {
                availablePlaces.Foreground = Brushes.Green;

            }
            availablePlaces.Content = SelectedTour.AvailablePlaces;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(NumberOfParticipants.Text == "")
            {
                MessageBox.Show("Please insert number of participants");
            }
            else
            {
                Close();
                InsertedNumberOfParticipants = Convert.ToInt32(NumberOfParticipants.Text);
                if(SelectedTour.AvailablePlaces < InsertedNumberOfParticipants)
                {
                    MessageBox.Show("The selected tour has no more free places, please select another one", "Tour reservation error", MessageBoxButton.OK, MessageBoxImage.Information);
                    AvailableToursWindow availableToursWindow = new AvailableToursWindow(SelectedTour, InsertedNumberOfParticipants);
                    availableToursWindow.ShowDialog();
                }
                else
                {
                    TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, InsertedNumberOfParticipants);
                    tourReservationWindow.ShowDialog();
                }
            }
        }

        private void NumberOfParticipants_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDigit(e);
        }
        private bool IsDigit(TextCompositionEventArgs e)
        {
            // ovaj regex cu trebati da izmenim jos
            Regex digitregex = new Regex("^\\b(100|\\d{1,2})\\b$");
            return !digitregex.IsMatch(e.Text);
        }
        private void NumberOfParticipants_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "1";
                return;
            }
        }

        private void SeeOtherButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            InsertedNumberOfParticipants = Convert.ToInt32(NumberOfParticipants.Text);
            AvailableToursWindow availableToursWindow = new AvailableToursWindow(SelectedTour, InsertedNumberOfParticipants);
            availableToursWindow.ShowDialog();

        }
    }
}
