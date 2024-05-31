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

        

        public CreatedReservationsPDFGeneratorEnglish CreatedReservationsPDFGeneratorEnglish { get; set; }
        public CreatedReservationsPDFGeneratorSerbian CreatedReservationsPDFGeneratorSerbian { get; set; }
        public ReservationReportViewModel(User user, Frame frame, ReservationReportPage page)
        {
            User = user;
            Frame = frame;
            Page = page;
            CreateReportCommand = new GuestICommand(OnGeneratePdf);
           
            


        }

        private void OnGeneratePdf()
        {
            if (!string.IsNullOrEmpty(Page.txtStartDate.Text))
                StartDate = Convert.ToDateTime(Page.txtStartDate.Text);

            if (!string.IsNullOrEmpty(Page.txtEndDate.Text))
                EndDate = Convert.ToDateTime(Page.txtEndDate.Text);
            MessageBox.Show((Page.langLabel.Content.ToString()));
            if(Page.langLabel.Content.ToString() == "English")
            {
                CreatedReservationsPDFGeneratorEnglish = new CreatedReservationsPDFGeneratorEnglish(User, StartDate, EndDate);
                CreatedReservationsPDFGeneratorEnglish.MakeCreatedReservationsReport();
            }

            if (Page.langLabel.Content.ToString() == "Srpski")
            {
                MessageBox.Show("uslo");
                CreatedReservationsPDFGeneratorSerbian = new CreatedReservationsPDFGeneratorSerbian(User, StartDate, EndDate);
                CreatedReservationsPDFGeneratorSerbian.MakeCreatedReservationsReport();
            }


        }

   
    }
}
