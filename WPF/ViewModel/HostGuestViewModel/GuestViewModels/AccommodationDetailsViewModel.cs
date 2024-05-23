using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AccommodationDetailsViewModel : INotifyPropertyChanged
    {
        public Frame Frame { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public User User { get; set; }

        public GuestICommand ChangedPictureCommand { get; set; }

        public GuestICommand ReserveCommand { get; set; }
        public AccommodationService AccommodationService { get; set; }


        private string firstPicture;
        public string FirstPicture
        {
            get { return firstPicture; }
            set
            {
                if (firstPicture != value)
                {

                    firstPicture = value;
                    OnPropertyChanged("FirstPicture");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

       
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public AccommodationDetailsViewModel(Frame frame, User user, AccommodationViewModel selectedAccommodation)
        {
            Frame = frame;
            User = user;
            SelectedAccommodation = selectedAccommodation;
            ChangedPictureCommand = new GuestICommand(ChangePicture);
            ReserveCommand = new GuestICommand(OnReserve);
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            FirstPicture = SelectedAccommodation.FirstPicture;
           
          
        }

        private void ChangePicture()
        {
            if (SelectedAccommodation.NumberOfPictures > 1)
            {
               
                AccommodationService.ChangeListOrder(SelectedAccommodation);
                FirstPicture = "../../" + SelectedAccommodation.FirstPicture;
                


            }
          
        }

      

        private void OnReserve()
        {
          
            Frame.Content = new ReservationInfoPage(SelectedAccommodation, User, Frame);
        }
    }
}
