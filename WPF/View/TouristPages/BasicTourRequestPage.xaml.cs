using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.TouristPages
{
    /// <summary>
    /// Interaction logic for BasicTourRequestPage.xaml
    /// </summary>
    public partial class BasicTourRequestPage : Page
    {
        TourRequestWindowViewModel TourRequest { get; set; }
        public BasicTourRequestPage(TourRequestWindowViewModel tourRequest)
        {
            InitializeComponent();
            TourRequest = tourRequest;
            DataContext = TourRequest;

            TourRequest.InitializeTourRequestWindow();
        }
        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Integer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }
        private void AddParticipant_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TourRequest.AddParticipantCommand.CanExecute(null);
        }

        private void AddParticipant_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.AddParticipantCommand.Execute(null);
        }

        private void ParticipantName_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ParticipantName_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NameTextBox.Focus();
        }

        private void CountryFocus_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CountryFocus_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CountryComboBox.Focus();
        }
    }
}
