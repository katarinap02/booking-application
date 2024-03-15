using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace BookingApp.View.GuideWindows
{
    public partial class GuideWithTourWindow : Window
    {
        private ObservableCollection<Checkpoint> checkpointsWithColors;
        private int currentCheckpointIndex;

        public GuideWithTourWindow()
        {
            InitializeComponent();

            // Sample data for demonstration
            var checkpoints = new[] { "Checkpoint A", "Checkpoint B", "Checkpoint C" };

            // Create Checkpoint objects with colors
            checkpointsWithColors = new ObservableCollection<Checkpoint>();
            SolidColorBrush activeColor = Brushes.Green;
            SolidColorBrush inactiveColor = Brushes.Gray;
            foreach (var checkpoint in checkpoints)
            {
                checkpointsWithColors.Add(new Checkpoint { Name = checkpoint, IndicatorColor = inactiveColor });
            }

            // Set the sample data as the DataContext
            DataContext = new
            {
                CheckpointsWithColors = checkpointsWithColors,
                SelectedCheckpointDescription = "Description of currently selected checkpoint"
            };

            // Initialize current checkpoint index
            currentCheckpointIndex = 0;
            UpdateCheckpointIndicators();
        } 

        private void NextCheckpointButton_Click(object sender, RoutedEventArgs e)
        {
            // Increment current checkpoint index
            currentCheckpointIndex++;

            // Update visual indicators
            UpdateCheckpointIndicators();
        }

        private void UpdateCheckpointIndicators()
        {
            for (int i = 0; i < checkpointsWithColors.Count; i++)
            {
                if (i == currentCheckpointIndex)
                {
                    checkpointsWithColors[i].IndicatorColor = Brushes.Green; // Set color for current checkpoint
                }
                else
                {
                    checkpointsWithColors[i].IndicatorColor = Brushes.Gray; // Set color for other checkpoints
                }
            }
        }

        private void FinishTourButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to finish the tour
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
