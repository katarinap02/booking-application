using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TourDetailsWindow.xaml
    /// </summary>
    public partial class TourDetailsWindow : Window
    {
        public TourViewModel Tour { get; set; }
        private int checkpointIndex;
        public ObservableCollection<Checkpoint> checkpointWithColors {  get; set; }
        public List<string> ImagePaths { get; set; }
        public bool IsMyTour;

        public TourDetailsWindow(TourViewModel selectedTour, bool isMyTour)
        {
            InitializeComponent();
            DataContext = this;
            Tour = selectedTour;
            IsMyTour = isMyTour;
            ImagePaths = Tour.Pictures;
            checkpointWithColors = new ObservableCollection<Checkpoint>();


            WindowInitialization();
        }

        public void WindowInitialization()
        {
            SolidColorBrush activeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#56707a"));
            SolidColorBrush inactiveColor = Brushes.Gray;
            foreach (var checkpoint in Tour.Checkpoints)
            {
                checkpointWithColors.Add(new Checkpoint { Name = checkpoint, IndicatorColor = inactiveColor });
            }

            if (IsMyTour)
            {
                PdfPanel.Visibility = Visibility.Visible;
            }
            checkpointIndex = Tour.CurrentCheckpoint;
            for(int i = 0; i < Tour.Checkpoints.Count; i++)
            {
                if(i == checkpointIndex)
                {
                    checkpointWithColors[i].IndicatorColor = activeColor;
                }
                else
                {
                    checkpointWithColors[i].IndicatorColor = inactiveColor;
                }
            }
            // images
            if(ImagePaths != null)
            {
                for (int i = 0; i < ImagePaths.Count; i++)
                {
                    ImagePaths[i] = "../../" + ImagePaths[i];
                }
            }
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {

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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
