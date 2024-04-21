using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.View.Guest.GuestTools;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ShowAccommodationsViewModel : IObserver
    {
        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }


        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public User User { get; set; }

        public Frame Frame { get; set; }

        public AccommodationsPage AccommodationsPage { get; set; }

        public HostService HostService { get; set; }

        public AccommodationSearcher AccommodationSearcher { get; set; }





        public ShowAccommodationsViewModel(User user, Frame frame, AccommodationsPage accommodationsPage)
        {

            Accommodations = new ObservableCollection<AccommodationViewModel>();
            AccommodationService = new AccommodationService();
            AccommodationService.Subscribe(this);
            User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;

            Frame = frame;
            AccommodationReservationService = new AccommodationReservationService();
            HostService = new HostService();
            AccommodationsPage = accommodationsPage;
            AccommodationSearcher = new AccommodationSearcher(AccommodationService);

        }


        public void Update()
        {
            Accommodations.Clear();
            List<AccommodationViewModel> superHostAccommodations = new List<AccommodationViewModel>();
            List<AccommodationViewModel> nonSuperHostAccommodations = new List<AccommodationViewModel>();

            SeparateAccommodations(AccommodationService, superHostAccommodations, nonSuperHostAccommodations);

            foreach (AccommodationViewModel superHostAccommodation in superHostAccommodations)
            {
                Accommodations.Add(superHostAccommodation);

            }

            foreach (AccommodationViewModel nonSuperHostAccommodation in nonSuperHostAccommodations)
            {
                Accommodations.Add(nonSuperHostAccommodation);

            }

        }
        public void SeparateAccommodations(AccommodationService accommodationService, List<AccommodationViewModel> superHostAccommodations, List<AccommodationViewModel> nonSuperHostAccommodations)
        {
            foreach (Accommodation accommodation in AccommodationService.GetAll())
            {

                AccommodationViewModel accommodationDTO = new AccommodationViewModel(accommodation);
                Host host = HostService.GetById(accommodation.HostId);
                HostService.BecomeSuperHost(host);
                accommodationDTO.IsSuperHost = host.IsSuperHost;

                if (accommodationDTO.IsSuperHost)
                    superHostAccommodations.Add(accommodationDTO);
                else
                    nonSuperHostAccommodations.Add(accommodationDTO);

                //MessageBox.Show(Accommodations[0].Type.ToString());
            }
        }

        public void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SelectedAccommodation = button.DataContext as AccommodationViewModel;
            Frame.Content = new ReservationInfoPage(SelectedAccommodation, User, Frame);

        }

        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> queries = new List<string>();
            queries.Add(AccommodationsPage.txtSearchName.Text); //nameQuery
            queries.Add(AccommodationsPage.txtSearchCity.Text); //cityQuery
            queries.Add(AccommodationsPage.txtSearchCountry.Text); //countryQuery
            queries.Add(AccommodationsPage.txtSearchType.Text); //typeQuery
            queries.Add(AccommodationsPage.txtSearchGuestNumber.Text); //guestQuery
            queries.Add(AccommodationsPage.txtSearchReservationDays.Text); //reservationQuery

            AccommodationsPage.AccommodationListBox.ItemsSource = AccommodationSearcher.SearchAccommodations(queries);

        }


    }
}
