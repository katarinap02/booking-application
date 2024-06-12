﻿using BookingApp.WPF.ViewModel.GuideTouristViewModel;
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
    /// Interaction logic for ComplexTourRequestPage.xaml
    /// </summary>
    public partial class ComplexTourRequestPage : Page
    {
        TourRequestWindowViewModel TourRequest { get; }
        public ComplexTourRequestPage(TourRequestWindowViewModel tourRequest)
        {
            InitializeComponent();
            TourRequest = tourRequest;
            DataContext = TourRequest;

        }
        private void Integer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }
        private void AddTour_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TourRequest.AddTourCommand.CanExecute(null);
        }

        private void AddTour_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.AddTourCommand.Execute(null);
        }
        private void NameFocus_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NameFocus_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NameTextBox.Focus();
        }
    }
}
