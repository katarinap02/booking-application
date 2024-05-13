using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.HostPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
using BookingApp.Domain.Model.Features;
using System.Windows.Media.Animation;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {

        public HostWindowViewModel hostPageViewModel { get; set; }

        public HostWindow(User user)
        {

            InitializeComponent();
            hostPageViewModel = new HostWindowViewModel(user, this.HostFrame.NavigationService);
            this.DataContext = hostPageViewModel;

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
    }
    }
