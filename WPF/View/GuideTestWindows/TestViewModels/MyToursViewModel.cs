using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class MyToursViewModel : ViewModelBase
    {
        private string _buttonString;
        public string ButtonString
        {
            get { return _buttonString; }
            set
            {
                _buttonString = value;
                OnPropertyChanged(nameof(ButtonString));
            }
        }

        private ObservableCollection<TourViewModel> tourViewModels; // moje ture ili ture za cancel-ovanje
        public ObservableCollection<TourViewModel> TourViewModels
        {
            get { return tourViewModels; }
            set
            {
                tourViewModels = value;
                OnPropertyChanged(nameof(TourViewModels));
            }
        }     
        public TourViewModel SelectedTour { get; set; }
        private readonly TourService tourService;
        public bool IsCurrentlyShowCancelledTours;
        private int GuideId;

        public MyICommand SwitchTours {  get; set; }
        public MyICommand Cancel { get; set; }
        public MyICommand AddTour { get; set; }
        public MyICommand AddTourDate { get; set; }

        public MyToursViewModel(int id) {
            GuideId = id;
            tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            IsCurrentlyShowCancelledTours = false;
            TourViewModels = new ObservableCollection<TourViewModel>();
            getToursByGuide(id);
            ButtonString = "  Show tours that can be cancelled  ";
            SwitchTours = new MyICommand(switchTours);
            Cancel = new MyICommand(CancelTour);
            AddTour = new MyICommand(AddNewTour);
            AddTourDate = new MyICommand(AddNewTourDate);
        }

        public void AddNewTour() {
            AddingTourWindow addingTourWindow = new AddingTourWindow(GuideId);
            addingTourWindow.ShowDialog();
        }

        public void AddNewTourDate()
        {
            MessageBox.Show("Not implemented yet");
        }

        public void switchTours()
        {
            if(IsCurrentlyShowCancelledTours) 
            {                
                getToursByGuide(GuideId);
                ButtonString = "  Show tours that can be cancelled  ";
            }
            else if(!IsCurrentlyShowCancelledTours) 
            {                
                getToursToCancel(GuideId);
                ButtonString = "  Show all of your tours  ";
            }
        }

        private void getToursToCancel(int guideId)
        {
            IsCurrentlyShowCancelledTours = true;
            TourViewModels.Clear();
            List<Tour> tours = tourService.findToursToCancel(guideId);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            TourViewModels = newViewModels;
        }

        private void getToursByGuide(int guideId)
        {
            IsCurrentlyShowCancelledTours = false;
            TourViewModels.Clear();
            List<Tour> tours = tourService.getToursByGuide(guideId);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            TourViewModels = newViewModels;
        }

        private void CancelTour() {
            if (SelectedTour != null)
            {
                if (CanCancel())
                {
                    tourService.cancelTour(SelectedTour.Id, GuideId);
                    UpdateData();
                }
                else
                {
                    MessageBox.Show("Selected Tour can't be  cancelled, as its beggining is in less than 48 hours from now.");
                }
                
            }
            else
            {
                MessageBox.Show("Please select a tour to cancel!");
            }
        }

        private void UpdateData() {
            if (IsCurrentlyShowCancelledTours) // ako trenutno prikazujem cancelovane prikaxi sve
            {
                getToursToCancel(GuideId);
            }
            else if (!IsCurrentlyShowCancelledTours) // ako trenutno prikazujem sve prikaxi cancelovane
            {

                getToursByGuide(GuideId);                
            }
        }

        private bool CanCancel() {
            
            TimeSpan difference = SelectedTour.Date - DateTime.Now;
            if (difference.TotalHours <= 48 || SelectedTour.Status != TourStatus.inPreparation || SelectedTour.Date < DateTime.Now)
            {
                return false;
            }
            return true;
        }



    }
}
