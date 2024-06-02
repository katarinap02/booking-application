using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.Guest.GuestPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using BookingApp.WPF.ViewModel.Commands;
using System.IO;
using System.Windows;
using BookingApp.WPF.View.Guest.GuestTools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.GuestPages;
using System.Security.Cryptography;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationReportViewModel
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReservationReportPage Page { get; set; }

        public GuestICommand CreateReportCommand {  get; set; }

        public GuestICommand YourProfileCommand { get; set; }
      

        public string AccommodationName { get; set; }
        public string Location { get; set; }

        public CreatedReservationsPDFGeneratorEnglish CreatedReservationsPDFGeneratorEnglish { get; set; }
        public CreatedReservationsPDFGeneratorSerbian CreatedReservationsPDFGeneratorSerbian { get; set; }

        public CancelledReservationsPDFGeneratorEnglish CancelledReservationsPDFGeneratorEnglish { get; set; }
        public CancelledReservationsPDFGeneratorSerbian CancelledReservationsPDFGeneratorSerbian { get; set; }
        public ReservationReportViewModel(User user, Frame frame, ReservationReportPage page)
        {
            User = user;
            Frame = frame;
            Page = page;
            CreateReportCommand = new GuestICommand(OnGeneratePdf, CanGenerate);
            YourProfileCommand = new GuestICommand(OnYourProfile);
           

        }

        private bool CanGenerate()
        {
            DateTime start;
            DateTime end;
            start = SetUpStart();
            end = SetUpEnd();
            
            ToggleDateValidationMessage();
            if (ValidateDateInputs(start, end))
                return true;
            else
                return false;
        }

        private void ToggleDateValidationMessage()
        {
            DateTime start;
            DateTime end;
            start = SetUpStart();
            end = SetUpEnd();
            if (!ValidateDateInputs(start, end))
            {
                Page.dateValidator.Visibility = Visibility.Visible;
                Page.txtStartDate.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Page.txtStartDate.BorderThickness = new Thickness(2);
                Page.txtEndDate.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Page.txtEndDate.BorderThickness = new Thickness(2);
            }
            else
            {
                Page.dateValidator.Visibility = Visibility.Hidden;

                Page.txtStartDate.BorderBrush = SystemColors.ControlDarkBrush;
                Page.txtStartDate.BorderThickness = new Thickness(1);
                Page.txtEndDate.BorderBrush = SystemColors.ControlDarkBrush;
                Page.txtEndDate.BorderThickness = new Thickness(1);
            }

        }

        private DateTime SetUpStart()
        {
            if (string.IsNullOrEmpty(Page.txtStartDate.Text))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(Page.txtStartDate.Text);
        }
        private DateTime SetUpEnd()
        {
            if (string.IsNullOrEmpty(Page.txtEndDate.Text))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(Page.txtEndDate.Text);
        }


        private bool ValidateDateInputs(DateTime start, DateTime end)
        {
            if (start >= end)
                return false;

            else
                return true;



        }
        private void OnYourProfile()
        {
            Frame.Content = new ProfileInfo(User, Frame);
        }

        private void OnGeneratePdf()
        {
            if (!string.IsNullOrEmpty(Page.txtStartDate.Text))
                StartDate = Convert.ToDateTime(Page.txtStartDate.Text);

            if (!string.IsNullOrEmpty(Page.txtEndDate.Text))
                EndDate = Convert.ToDateTime(Page.txtEndDate.Text);
            
            if(Page.langLabel.Content.ToString() == "English")
            {
                if(Page.createdRadioBtn.IsChecked == true)
                {
                    CreatedReservationsPDFGeneratorEnglish = new CreatedReservationsPDFGeneratorEnglish(User, StartDate, EndDate);
                    CreatedReservationsPDFGeneratorEnglish.MakeCreatedReservationsReport();
                    Frame.Content = new ReportSuccessfulPage(User, Frame, CreatedReservationsPDFGeneratorEnglish.SavedPath);

                }

                if (Page.cancelledRadioBtn.IsChecked == true)
                {
                    CancelledReservationsPDFGeneratorEnglish = new CancelledReservationsPDFGeneratorEnglish(User, StartDate, EndDate);
                    CancelledReservationsPDFGeneratorEnglish.MakeCancelledReservationsReport();
                    Frame.Content = new ReportSuccessfulPage(User, Frame, CancelledReservationsPDFGeneratorEnglish.SavedPath);

                }

            }

            if (Page.langLabel.Content.ToString() == "Srpski")
            {
                if (Page.createdRadioBtn.IsChecked == true)
                {

                    CreatedReservationsPDFGeneratorSerbian = new CreatedReservationsPDFGeneratorSerbian(User, StartDate, EndDate);
                    CreatedReservationsPDFGeneratorSerbian.MakeCreatedReservationsReport();
                    Frame.Content = new ReportSuccessfulPage(User, Frame, CreatedReservationsPDFGeneratorSerbian.SavedPath);
                }


                if (Page.cancelledRadioBtn.IsChecked == true)
                {
                    CancelledReservationsPDFGeneratorSerbian = new CancelledReservationsPDFGeneratorSerbian(User, StartDate, EndDate);
                    CancelledReservationsPDFGeneratorSerbian.MakeCancelledReservationsReport();
                    Frame.Content = new ReportSuccessfulPage(User, Frame, CancelledReservationsPDFGeneratorSerbian.SavedPath);
                }

            }


        }

   
    }
}
