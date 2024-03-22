using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    public partial class TourReservationWindow : Window
    {
        public Tour SelectedTour { get; set; }
        private readonly TourParticipantRepository _tourParticipantRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourRepository _tourRepository;
        public TourReservation TourReservation { get; set; }
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
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();

            TourReservation = new TourReservation();
            TourParticipant = new TourParticipant();

            TourParticipantDTOs = new List<TourParticipantDTO>();
            TourParticipantsListBox = new List<TourParticipantDTO>();
            InitializeTourDetailsLabels();
            InitializeParticipantInformationGroupBox();

        }

        private void InitializeParticipantInformationGroupBox()
        {
            if (Convert.ToInt32(ParticipantCount) == 0)
                setGroupBoxVisibility(false);
            else
                setGroupBoxVisibility(true);
        }

        private void InitializeTourDetailsLabels()
        {
            availablePlaces.Foreground = Brushes.Green;
            availablePlaces.Content = SelectedTour.AvailablePlaces;
            tourName.Content = SelectedTour.Name;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            int participantCount = Convert.ToInt32(ParticipantCount);
            if (participantCount < TourParticipantDTOs.Count)
            {
                MessageBox.Show("Too many participants in the list of participants", "Participants error", MessageBoxButton.OK, MessageBoxImage.Warning);
            } 
            else if (participantCount > TourParticipantDTOs.Count)
            {
                MessageBox.Show("Too less participants in the list of participants", "Participants error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                saveParticipants();
                saveReservation();

                ReduceNumberOfAvailablePlaces();
                MessageBox.Show("Tour " + "\"" + SelectedTour.Name + "\"" + " booked!");
                Close();
            }
        }

        private void ReduceNumberOfAvailablePlaces()
        {
            _tourRepository.UpdateAvailablePlaces(SelectedTour, TourParticipantDTOs.Count);
        }
        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            if(!AllFieldsFilled(years.Text, name.Text, lastName.Text))
            {
                MessageBox.Show("All fields must be filled");
            }
            else
            {
                TourParticipantDTO tourParticipantDTO = saveParticipantToDTO(name.Text, lastName.Text, years.Text);
                TourParticipantsListBox.Add(tourParticipantDTO);

                SetupForNextParticipantInput();
            }
        }

        private void SetupForNextParticipantInput()
        {
            ParticipantsListBox.ItemsSource = null;
            ParticipantsListBox.ItemsSource = TourParticipantsListBox;

            name.Text = "";
            lastName.Text = "";
            years.Text = "";
        }

        private bool AllFieldsFilled(string years, string name, string lastName)
        {
            if (years == string.Empty || name == string.Empty || lastName == string.Empty)
            {
                return false;
            }

            return true;
        }

        private void saveReservation()
        {
            TourReservation.TourId = SelectedTour.Id;
            TourReservation.ParticipantIds = _tourParticipantRepository.GetAllIdsByReservation(_tourReservationRepository.NextId());
            TourReservation.StartCheckpoint = SelectedTour.currentCheckpoint;

            _tourReservationRepository.Add(TourReservation);
        }

        private void saveParticipants()
        {
            int reservationId = _tourReservationRepository.NextId();
            foreach (TourParticipantDTO tp in TourParticipantDTOs)
            {
                tp.ReservationId = reservationId;
                tp.Id = _tourParticipantRepository.NextId();
                _tourParticipantRepository.Add(tp.ToTourParticipant());
            }
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

        private void removeParticipant_Click(object sender, RoutedEventArgs e)
        {
            TourParticipantDTO selectedItem = (TourParticipantDTO)ParticipantsListBox.SelectedItem;
            TourParticipantDTOs.Remove(selectedItem);

            TourParticipantsListBox.Remove(selectedItem);
            ParticipantsListBox.ItemsSource = null;
            ParticipantsListBox.ItemsSource = TourParticipantsListBox;
        }

        private void ParticipantsMinus_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(ParticipantCount) > 0)
                ParticipantCount = (Convert.ToInt32(ParticipantCount) - 1).ToString();
            if(Convert.ToInt32(ParticipantCount) == 0)
                setGroupBoxVisibility(false);
        }



        private void ParticipantsPlus_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(ParticipantCount) < SelectedTour.AvailablePlaces)
            {
                ParticipantCount = (Convert.ToInt32(ParticipantCount) + 1).ToString();

                setGroupBoxVisibility(true);
            }
        }

        private void setGroupBoxVisibility(bool visibility)
        {
            if(visibility)
                ParticipantInformationGroupBox.Visibility = Visibility.Visible;
            else
                ParticipantInformationGroupBox.Visibility = Visibility.Collapsed;
        }
        private void YearsPlus_Click(object sender, RoutedEventArgs e)
        {
            if(years.Text == "")
                years.Text = "0";

            years.Text = (Convert.ToInt32(years.Text) + 1).ToString();
            
        }

        private void YearsMinus_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(years.Text) > 0)
            {
                years.Text = (Convert.ToInt32(years.Text) - 1).ToString();
            }
        }

        private void years_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDigit(e);
        }
        private bool IsDigit(TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                return true;

            return false;
        }

        private void years_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                return;
            }
        }
    }
}
