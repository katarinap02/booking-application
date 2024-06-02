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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            DateTime start;
            DateTime end;
            start = SetUpStartValid();
            end = SetUpEndValid();
            ToggleDateValidationMessage();

            if (ValidateGuestNumber(GuestNumber) && ValidateDayNumber(DayNumber) && ValidateDateInputs(start, end))
                return true;
            else
                return false;
        }
        private bool ValidateDateInputs(DateTime start, DateTime end)
        {
            if (start >= end && !string.IsNullOrEmpty(Page.txtEndDate.Text) && !string.IsNullOrEmpty(Page.txtStartDate.Text))   
                return false;

            else
                return true;



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

        private DateTime SetUpEndValid()
        {
            if (string.IsNullOrEmpty(Page.txtEndDate.Text))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(Page.txtEndDate.Text);
        }

        private DateTime SetUpStartValid()
        {
            if (string.IsNullOrEmpty(Page.txtStartDate.Text))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(Page.txtStartDate.Text);
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
