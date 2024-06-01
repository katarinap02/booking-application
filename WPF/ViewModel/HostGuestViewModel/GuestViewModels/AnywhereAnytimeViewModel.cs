using BookingApp.Domain.Model.Features;
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

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AnywhereAnytimeViewModel : INotifyPropertyChanged
    {
       
        public Frame Frame { get; set; }
        public User User { get; set; }
        public AnywhereAnytimePage Page { get; set; }

        // KOMANDE
        public GuestICommand ContinueCommand { get; set; }
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

                ContinueCommand.RaiseCanExecuteChanged();
            }

        }
        
        private int guestNumber;

        public int GuestNumber
        {
            get { return guestNumber; }
            set
            {
                if (guestNumber != value)
                {
                    guestNumber = value;
                    OnPropertyChanged("GuestNumber");

                }
                ContinueCommand.RaiseCanExecuteChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public AnywhereAnytimeViewModel(User user, Frame frame, AnywhereAnytimePage page)
        {
            User = user;
            Frame = frame;
           
            Page = page;
           
            ContinueCommand = new GuestICommand(OnContinue, CanContinue);
            DayNumber = 1;
            GuestNumber = 1;
        }

        private bool CanContinue()
        {
           
            
            if (ValidateGuestNumber(GuestNumber) && ValidateDayNumber(DayNumber))
                return true;
            else
                return false;
        }

       

        private bool ValidateDayNumber(int dayNumber)
        {
            if (DayNumber <= 0)
                return false;

            else
                return true;
        }

        private bool ValidateGuestNumber(int guestNumber)
        {
            if (GuestNumber <= 0)
                return false;

            else
                return true;
        }

        private void OnContinue()
        {

            DateTime startDate = SetUpStart();
            DateTime endDate = SetUpEnd();
            Frame.Content = new AnywhereAnytimeContinuePage(User, Frame, DayNumber, GuestNumber, startDate, endDate);
        }

        private DateTime SetUpEnd()
        {
            if (string.IsNullOrEmpty(Page.txtEndDate.Text))
                return DateTime.MaxValue;
            else
                return Convert.ToDateTime(Page.txtEndDate.Text);
        }

        private DateTime SetUpStart()
        {
            if (string.IsNullOrEmpty(Page.txtStartDate.Text))
                return DateTime.Now;
            else
                return Convert.ToDateTime(Page.txtStartDate.Text);
        }
    }
}
