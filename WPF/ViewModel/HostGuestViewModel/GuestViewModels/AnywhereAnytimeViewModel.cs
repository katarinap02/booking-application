using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AnywhereAnytimeViewModel : INotifyPropertyChanged
    {
        private int dayNumber;
        public int DayNumber
        {
            get { return dayNumber; }
            set
            {
                if (dayNumber != value)
                {
                    dayNumber = value;
                    OnPropertyChanged("DayNumber");

                }

                
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int guestNumber;

        public int GuestNumber {
            get { return guestNumber;  }
            set
            {
                if( guestNumber != value)
                {
                    guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                }
            }
        }  
        public Frame Frame { get; set; }
        public User User { get; set; }
        public AnywhereAnytimePage Page { get; set; }

        // KOMANDE
        public GuestICommand ContinueCommand { get; set; }


        public AnywhereAnytimeViewModel(User user, Frame frame, AnywhereAnytimePage page)
        {
            User = user;
            Frame = frame;
            DayNumber = 1;
            GuestNumber = 1;
            Page = page;
            Page.txtStartDate.Text = DateTime.Now.ToString();
            Page.txtEndDate.Text = DateTime.Now.ToString();
            ContinueCommand = new GuestICommand(OnContinue);
        }

        private void OnContinue()
        {
            DateTime startDate = Convert.ToDateTime(Page.txtStartDate.Text);
            DateTime endDate = Convert.ToDateTime((Page.txtEndDate.Text));
            Frame.Content = new AnywhereAnytimeContinuePage(User, Frame, DayNumber, GuestNumber, startDate, endDate);
        }
    }
}
