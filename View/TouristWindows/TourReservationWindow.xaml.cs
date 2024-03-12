using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window, INotifyPropertyChanged
    {
        private Tour SelectedTour;

        #region Property
        private int participantCount;


        public string ParticipantCount
        {
            get
            {
                return participantCount.ToString();
            }
            set
            {
                if(value !=  participantCount.ToString())
                {
                    participantCount = Convert.ToInt32(value);
                    OnPropertyChanged(nameof(participantCount));
                }
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public TourReservationWindow(Tour selectedTour, int insertedNumberOfParticipants)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedTour = selectedTour;
            ParticipantCount = insertedNumberOfParticipants.ToString();


            availablePlaces.Foreground = Brushes.Green;
            availablePlaces.Content = SelectedTour.AvailablePlaces;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
