using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.TouristViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestDetailsWindow.xaml
    /// </summary>
    public partial class ComplexTourRequestDetailsWindow : Window
    {
        ComplexTourRequestDetailsViewModel TourRequest {  get; set; }
        public ComplexTourRequestDetailsWindow(TourRequestViewModel selectedTourRequest)
        {
            InitializeComponent();
            TourRequest = new ComplexTourRequestDetailsViewModel();
            TourRequest = TourRequest.ToComplexDetailsViewModel(selectedTourRequest);
            DataContext = TourRequest;

            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
        }
        private void CloseWindow(CloseWindowMessage message)
        {
            if (this.IsActive == true)
            {
                Close();
            }
        }
    }
}
