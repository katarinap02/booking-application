using BookingApp.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using BookingApp.WPF.ViewModel;
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.GuideWindows
{
    public partial class GuideWithTourWindow : Window
    {
        private ObservableCollection<Checkpoint> checkpointsWithColors;
        private int currentCheckpointIndex;
        private TourRepository tourRepository;
        private GuidedTourRepository guidedTourRepository;
        private User Guide;
        public TourViewModel Tour { get; set; }

        public GuideWithTourWindow(TourViewModel tourDTO, User user)
        {
            InitializeComponent();

            Tour = tourDTO;
            tourRepository = new TourRepository();
            guidedTourRepository = new GuidedTourRepository();
            Guide = user;

            var checkpoints = tourDTO.Checkpoints;

            checkpointsWithColors = new ObservableCollection<Checkpoint>();
            SolidColorBrush activeColor = Brushes.Green;
            SolidColorBrush inactiveColor = Brushes.Gray;
            foreach (var checkpoint in checkpoints)
            {
                checkpointsWithColors.Add(new Checkpoint { Name = checkpoint, IndicatorColor = inactiveColor });
            }

            Informations.Text = "You are currently on checkpoint number " + (currentCheckpointIndex + 1) + " out of " + (checkpoints.Count);

            DataContext = new
            {
                CheckpointsWithColors = checkpointsWithColors
            };

            currentCheckpointIndex = Tour.CurrentCheckpoint;
            UpdateDesign();
            Close_Button.Visibility = Visibility.Collapsed;


            ActivateTourIfNotActive(tourDTO);

        }

        public void ActivateTourIfNotActive(TourViewModel tourDTO)
        {
            if (!guidedTourRepository.Exists(Guide.Id, Tour.Id))
            {
                guidedTourRepository.Add(Guide, tourDTO.ToTour());
                tourRepository.activateTour(tourDTO.Id);
            }
        }


        public void JoinTourist_Click(object sender, RoutedEventArgs e)
        {
            TouristListWindow touristListWindow = new TouristListWindow(Tour.Id, currentCheckpointIndex);
            touristListWindow.ShowDialog();
        }

        private void NextCheckpointButton_Click(object sender, RoutedEventArgs e)
        {
            currentCheckpointIndex++;
            tourRepository.nextCheckpoint(Tour.Id);

            if(currentCheckpointIndex == Tour.Checkpoints.Count()-1)
            {
                UpdateDesign();
                MessageBox.Show("You have reached the final checkpoint. The tour has been finnished.");
                FinnishTour();
            }
            else if (currentCheckpointIndex < Tour.Checkpoints.Count() - 1)
            {
                UpdateDesign();
            }
                        
        }

        public void FinnishTour() {
            tourRepository.finnishTour(Tour.Id);
            FinnishTour_Button.Visibility = Visibility.Collapsed;
            TouristJoined_Button.Visibility = Visibility.Collapsed;
            Next_Button.Visibility = Visibility.Collapsed;
            Close_Button.Visibility = Visibility.Visible;

            guidedTourRepository.Remove(Guide.Id, Tour.Id);
        }

        private void UpdateDesign()
        {
            Informations.Text = "You are currently on checkpoint number " + (currentCheckpointIndex + 1) + " out of " + (Tour.Checkpoints.Count);
            for (int i = 0; i < checkpointsWithColors.Count; i++)
            {
                if (i == currentCheckpointIndex)
                {
                    checkpointsWithColors[i].IndicatorColor = Brushes.Green; 
                }
                else
                {
                    checkpointsWithColors[i].IndicatorColor = Brushes.Gray; 
                }
            }
        }

        private void FinishTourButton_Click(object sender, RoutedEventArgs e)
        {
            FinnishTour();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            GuideMainWindow guideMainWindow = new GuideMainWindow(Guide);
            guideMainWindow.Show();
            Close();
        }

    }

    public class Checkpoint : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private Brush indicatorColor;
        public Brush IndicatorColor
        {
            get { return indicatorColor; }
            set { indicatorColor = value; NotifyPropertyChanged(nameof(IndicatorColor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
