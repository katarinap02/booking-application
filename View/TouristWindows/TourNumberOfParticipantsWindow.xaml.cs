using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour {  get; set; }
        private int InsertedNumberOfParticipants;
        private readonly TourRepository _repository;


        public TourNumberOfParticipantsWindow(Tour selectedTour)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedTour = selectedTour;

            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetTourByCountryWithAvailablePlaces(SelectedTour.Country));

            NumberOfParticipants.Text = "1";

            InitializeWindow();


            availablePlaces.Content = SelectedTour.AvailablePlaces;
        }

        private void InitializeWindow()
        {
            if (SelectedTour.AvailablePlaces == 0)
            {
                availablePlaces.Foreground = Brushes.Red;
                if (Tours.Count > 0)
                {
                    DataTabVisible(true);
                    SetWindowSize("big");
                }
                CloseButton.HorizontalAlignment = HorizontalAlignment.Center;
                ConfirmButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                availablePlaces.Foreground = Brushes.Green;

                DataTabVisible(false);
                SetWindowSize("small");
                CloseButton.HorizontalAlignment = HorizontalAlignment.Left;
                ConfirmButton.Visibility = Visibility.Visible;
            }
        }

        private void DataTabVisible(bool visible)
        {
            if (visible)
                OtherToursTabItem.Visibility = Visibility.Visible;
            else
                OtherToursTabControl.Visibility = Visibility.Collapsed;
        }

        private void SetWindowSize(string size)
        {
            if (size.Equals("small"))
            {
                this.Width = 600;
                this.Height = 220;
            }
            else
            {
                this.Width = 800;
                this.Height = 600;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            InsertedNumberOfParticipants = Convert.ToInt32(NumberOfParticipants.Text);
            TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, InsertedNumberOfParticipants);
            tourReservationWindow.ShowDialog();
            Close();
        }

        private void NumberOfParticipants_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDigit(e);
        }
        private bool IsDigit(TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                return true;

            return false;
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

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            InsertedNumberOfParticipants = Convert.ToInt32(NumberOfParticipants.Text);
            TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, InsertedNumberOfParticipants);
            tourReservationWindow.ShowDialog();
            Close();
        }
    }
}
