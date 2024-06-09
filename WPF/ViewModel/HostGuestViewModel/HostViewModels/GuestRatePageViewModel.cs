using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.HostPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class GuestRatePageViewModel : IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationReservationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }
        public AccommodationReservationRepository accommodationReservationRepository { get; set; }

        public HostService hostService { get; set; }

        public Host host { get; set; }

        private AccommodationReservationViewModel selectedAccommodation {  set; get; }
        public AccommodationReservationViewModel SelectedAccommodation
        {
            get => selectedAccommodation;
            set
            {
                if (selectedAccommodation != value)
                {
                    selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                }
            }
        }

        public GuestRateViewModel guestRateViewModel { get; set; }

        public GuestRateRepository guestRateRepository { get; set; }

        public MyICommand SaveCommand { get; set; }

        public User User { get; set; }

        public NavigationService NavService { get; set; }

        public bool IsDemo {  get; set; }

        public GuestRatePageViewModel(User user, NavigationService navigationService, bool isDemo)
        {
            Accommodations = new ObservableCollection<AccommodationReservationViewModel>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            guestRateRepository = new GuestRateRepository();
            guestRateViewModel = new GuestRateViewModel();
            User = user;
            NavService = navigationService;
            hostService = new HostService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            host = hostService.GetByUsername(user.Username);
            SaveCommand = new MyICommand(Save_Click);
            IsDemo = isDemo;
            if (IsDemo)
            {
                HandleTextBoxDemo();
            }
            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetGuestForRate())
            {
                if (accommodationRepository.GetById(accommodation.AccommodationId).HostId == host.Id)
                    Accommodations.Add(new AccommodationReservationViewModel(accommodation));

            }
        }

        private void Save_Click()
        {
            if (SelectedAccommodation != null)
            {
                guestRateViewModel.ReservationId = SelectedAccommodation.Id;
                guestRateViewModel.GuestId = SelectedAccommodation.GuestId;
                guestRateViewModel.AccommodationId = SelectedAccommodation.AccommodationId;
                guestRateRepository.Add(guestRateViewModel.toGuestRate());
                MessageBox.Show("Guest rate added.");
            }
            else
            {
                MessageBox.Show("Please select guest.");
            }
            Update();
            GuestRatePage page = new GuestRatePage(User, NavService, IsDemo);
            this.NavService.Navigate(page);

        }

        private void HandleTextBoxDemo()
        {
            AddOnTextBox(8.3, "", 2);
            AddOnTextBox(13.5, "", 3);
            AddOnTextBox(13.7, "", 4);
            AddOnTextBox(13.9, "N", 1);
            AddOnTextBox(14.1, "e", 1);
            AddOnTextBox(14.3, "a", 1);
            AddOnTextBox(14.5, "t", 1);
            AddOnTextBox(14.7, " ", 1);
            AddOnTextBox(14.9, "a", 1);
            AddOnTextBox(15.1, "n", 1);
            AddOnTextBox(15.3, "d", 1);
            AddOnTextBox(15.5, " ", 1);
            AddOnTextBox(15.7, "p", 1);
            AddOnTextBox(15.9, "l", 1);
            AddOnTextBox(16.1, "e", 1);
            AddOnTextBox(16.3, "a", 1);
            AddOnTextBox(16.5, "s", 1);
            AddOnTextBox(16.7, "a", 1);
            AddOnTextBox(16.9, "n", 1);
            AddOnTextBox(17.1, "t", 1);
            AddOnTextBox(17.3, " ", 1);
            AddOnTextBox(17.5, "g", 1);
            AddOnTextBox(17.7, "u", 1);
            AddOnTextBox(17.9, "e", 1);
            AddOnTextBox(18.1, "s", 1);
            AddOnTextBox(18.3, "t", 1);
            AddOnTextBox(18.3, ".", 1);
        }

        private void AddOnTextBox(double seconds, string letter, int num)
        {
            if (IsDemo)
            {
                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(seconds)
                };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    if (IsDemo && num == 1)
                    {
                        guestRateViewModel.AdditionalComment = guestRateViewModel.AdditionalComment + letter;
                    }
                    else if (IsDemo && num == 2 && Accommodations.Count > 0)
                    {
                        SelectedAccommodation = Accommodations[0];

                    }
                    else if (IsDemo && num == 3)
                    {
                        guestRateViewModel.IsChecked5 = true;
                    }
                    else if (IsDemo && num == 4)
                    {
                        guestRateViewModel.IsCheckedRules5 = true;
                    }



                };
                timer.Start();
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
