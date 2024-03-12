using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
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
        private readonly TourParticipantRepository _tourParticipantRepository;
        public TourParticipant TourParticipant { get; set; }
        public List<TourParticipantDTO> TourParticipantDTOs { get; set; }
        public List<TourParticipantDTO> TourParticipantsListBox { get; set; }

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
            _tourParticipantRepository = new TourParticipantRepository();
            TourParticipant = new TourParticipant();
            TourParticipantDTOs = new List<TourParticipantDTO>();
            TourParticipantsListBox = new List<TourParticipantDTO>();


            availablePlaces.Foreground = Brushes.Green;
            availablePlaces.Content = SelectedTour.AvailablePlaces;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {

            // proveriti da li ima toliko participanta koliko pise u tom polju

            //saveParticipants();
            //saveReservation();
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = name.Text;
            string LastName = lastName.Text;
            string Years = years.Text;
            if (Years == string.Empty || Name == string.Empty || LastName == string.Empty)
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                TourParticipantDTO tourParticipantDTO = saveParticipantToDTO(Name, LastName, Years);
                TourParticipantsListBox.Add(tourParticipantDTO);

                ParticipantsListBox.ItemsSource = null;
                ParticipantsListBox.ItemsSource = TourParticipantsListBox;

                name.Text = "";
                lastName.Text = "";
                years.Text = "";
            }
        }

        private void saveParticipants()
        {
            foreach (TourParticipantDTO tp in TourParticipantDTOs)
                _tourParticipantRepository.Add(tp.ToTourParticipant());
        }
        private TourParticipantDTO saveParticipantToDTO(string name, string lastName, string years)
        {
            TourParticipant.Name = name;
            TourParticipant.LastName = lastName;
            TourParticipant.Years = Convert.ToInt32(years);
            TourParticipant.Id = _tourParticipantRepository.NextId();
            TourParticipantDTO tourParticipantDTO = new TourParticipantDTO(TourParticipant);

            TourParticipantDTOs.Add(tourParticipantDTO);
            return tourParticipantDTO;
        }

        private void years_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDigit(e);
        }

        private bool IsDigit(TextCompositionEventArgs e)
        {
            if(!char.IsDigit(e.Text, e.Text.Length - 1))
                return true;

            return false;
        }

        private void removeParticipant_Click(object sender, RoutedEventArgs e)
        {
            TourParticipantDTO selectedItem = (TourParticipantDTO)ParticipantsListBox.SelectedItem;
            TourParticipantDTOs.Remove(selectedItem);

            TourParticipantsListBox.Remove(selectedItem);
            ParticipantsListBox.ItemsSource = null;
            ParticipantsListBox.ItemsSource = TourParticipantsListBox;
        }
    }
}
