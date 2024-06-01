using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class NotificationDetailsViewModel : INotifyPropertyChanged
    {
        public DelayRequestViewModel SelectedRequest { get; set; }

        public HostService HostService { get; set; }
        public string HostUsername { get; set; }

        public string OldDateRange { get; set; }
        public string NewDateRange { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfDays { get; set; }

        public NotificationDetailsPage Page { get; set; }

        private string requestHeader;
        public string RequestHeader
        {
            get { return requestHeader; }
            set
            {
                if (requestHeader != value)
                {

                    requestHeader = value;
                    OnPropertyChanged("RequestHeader");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AccommodationViewModel Accommodation { get; set; }

        public DelayRequestService DelayRequestService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public AccommodationService AccommodationService { get; set; }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {

                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        public ComboBox RequestStatusBox { get; set; }
        public GuestICommand BackCommand { get; set; }


        public NotificationDetailsViewModel(DelayRequestViewModel selectedRequest, NotificationDetailsPage page)
        {

            SelectedRequest = selectedRequest;
            DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            AccommodationReservation reservation = AccommodationReservationService.GetById(SelectedRequest.ReservationId);
            Accommodation = new AccommodationViewModel(AccommodationService.GetById(reservation.AccommodationId));
            HostUsername = HostService.GetById(SelectedRequest.HostId).Username;
            OldDateRange = SelectedRequest.StartLastDate.ToString("MM/dd/yyyy") + " -> " + selectedRequest.EndLastDate.ToString("MM/dd/yyyy");
            NewDateRange = SelectedRequest.StartDate.ToString("MM/dd/yyyy") + " -> " + SelectedRequest.EndDate.ToString("MM/dd/yyyy");
            NumberOfPeople = reservation.NumberOfPeople;
            Comment = SelectedRequest.Comment;
            NumberOfDays = (reservation.EndDate - reservation.StartDate).Days + 1;
            Page = page;
            UpdateHeaderContent();
            Page.langTextbox.TextChanged += ContentChanged;





        }

        private void ContentChanged(object sender, EventArgs e)
        {
            UpdateHeaderContent();
        }

        public void UpdateHeaderContent()
        {
            if (Page.langTextbox.Text == "English")
            { RequestHeader = CreateRequestHeaderEnglish(SelectedRequest); }
            if (Page.langTextbox.Text == "Srpski")
            { RequestHeader = CreateRequestHeaderSerbian(SelectedRequest); }

        }

        private string? CreateRequestHeaderEnglish(DelayRequestViewModel selectedRequest)
        {
            if (selectedRequest.Status == RequestStatus.PENDING)
            {
                Comment = "Waiting for host's response...";
                Page.requestHeader.Foreground = Brushes.Orange;
                Page.commentTxtBlock.Foreground = Brushes.Gray;
                return "Your request is pending.";

            }

            else if (selectedRequest.Status == RequestStatus.APPROVED)
            {
                if (Comment == "")
                {
                    Comment = "No comment";
                    Page.commentTxtBlock.Foreground = Brushes.Gray;
                }
                Page.requestHeader.Foreground = Brushes.LimeGreen;
                return "Your request is approved.";
            }

            else
            {
                if (Comment == "")
                {
                    Comment = "No comment";
                    Page.commentTxtBlock.Foreground = Brushes.Gray;
                }
                Page.requestHeader.Foreground = Brushes.Red;

                return "Your request is rejected";
            }

        }

        private string? CreateRequestHeaderSerbian(DelayRequestViewModel selectedRequest)
        {

            if (selectedRequest.Status == RequestStatus.PENDING)
            {
                Comment = "Čekanje komentara vlasnika...";
                Page.requestHeader.Foreground = Brushes.Orange;
                Page.commentTxtBlock.Foreground = Brushes.Gray;
                return "Vaš zahtev je na čekanju.";

            }

            else if (selectedRequest.Status == RequestStatus.APPROVED)
            {
                if (Comment == "")
                {
                    Comment = "Nema komentara";
                    Page.commentTxtBlock.Foreground = Brushes.Gray;
                }
                Page.requestHeader.Foreground = Brushes.LimeGreen;
                return "Vaš zahtev je prihvaćen.";
            }

            else
            {
                if (Comment == "")
                {
                    Comment = "Nema komentara";
                    Page.commentTxtBlock.Foreground = Brushes.Gray;
                }
                Page.requestHeader.Foreground = Brushes.Red;

                return "Vaš zahtev je odbijen.";
            }

        }
    }
}
