using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
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

namespace BookingApp.WPF.View.HostPages
{
    /// <summary>
    /// Interaction logic for PreviousRenovationDisplayPage.xaml
    /// </summary>
    public partial class PreviousRenovationDisplayPage : Page
    {
        PreviousRenovationDisplayPageViewModel ViewModel;
        public PreviousRenovationDisplayPage(User user, NavigationService navigationService)
        {
            ViewModel = new PreviousRenovationDisplayPageViewModel(user, navigationService);
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
