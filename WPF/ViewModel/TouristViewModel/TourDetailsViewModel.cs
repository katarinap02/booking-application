using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourDetailsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Checkpoint> CheckpointWithColors { get; set; }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => CloseWindow());
                }
                return _closeCommand;
            }
        }

        private void CloseWindow()
        {
            Messenger.Default.Send(new CloseWindowMessage());
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private Visibility _pdfPanel;
        public Visibility PdfPanel
        {
            get
            {
                return _pdfPanel;
            }
            set
            {
                if (_pdfPanel != value)
                {
                    _pdfPanel = value;
                    OnPropertyChanged(nameof(PdfPanel));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void TourDetailsWindowInitialization(bool IsMyTour)
        {
            InitializePdfPanel(IsMyTour);

            InitializeCheckpoints();

            InitializePictures();
        }
        private void InitializePictures()
        {
            if (SelectedTour.Pictures != null)
            {
                for (int i = 0; i < SelectedTour.Pictures.Count; i++)
                {
                    SelectedTour.Pictures[i] = "../../" + SelectedTour.Pictures[i];
                }
            }
        }
        private void InitializePdfPanel(bool IsMyTour)
        {
            PdfPanel = Visibility.Collapsed;
            PdfPanel = IsMyTour ? Visibility.Visible : PdfPanel;
        }
        private void InitializeCheckpoints()
        {
            SolidColorBrush activeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F4A09"));
            SolidColorBrush inactiveColor = Brushes.LightGray;
            CheckpointWithColors.Clear();
            foreach (var checkpoint in SelectedTour.Checkpoints)
            {
                CheckpointWithColors.Add(new Checkpoint { Name = checkpoint, IndicatorColor = inactiveColor });
            }

            int checkpointIndex = SelectedTour.CurrentCheckpoint;
            for (int i = 0; i < SelectedTour.Checkpoints.Count; i++)
            {
                if (i == checkpointIndex)
                {
                    CheckpointWithColors[i].IndicatorColor = activeColor;
                }
                else
                {
                    CheckpointWithColors[i].IndicatorColor = inactiveColor;
                }
            }
        }

        public TourDetailsViewModel()
        {
            CheckpointWithColors = new ObservableCollection<Checkpoint>();
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
}
