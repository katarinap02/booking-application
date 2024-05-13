using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BookingApp.View;
using BookingApp.WPF.ViewModel.GuideViewModel;

namespace BookingApp.WPF.View.GuideWindows
{
    /// <summary>
    /// Interaction logic for LanguageGraphWindow.xaml
    /// </summary>
    public partial class LanguageGraphWindow : Window
    {
        public LanguageGraphWindowViewModel ViewModel { get; set; }
        public LanguageGraphWindow(string language, int guide_id)
        {            
            ViewModel = new LanguageGraphWindowViewModel(language, guide_id);    
            DataContext = ViewModel;
            InitializeComponent();
        }

        
    }
}
