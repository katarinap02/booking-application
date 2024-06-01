using BookingApp.Repository;
using BookingApp.Application.Services;
using BookingApp.View.GuestPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.Guest.GuestPages;
using System.Windows.Controls.DataVisualization;
using BookingApp.WPF.ViewModel.Commands;
using System.Windows.Navigation;
using System.ComponentModel;

namespace BookingApp.View
{

    public partial class GuestWindow : Window
    {
      
        public User User { get; set; }

        private string currentLanguage;
        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
            }
        }
      
     

        public GuestICommand SwitchLanguageCommand { get; set; }

        public GuestICommand BackCommand { get; set; }
        public NavigationService NavigationService { get; set; }

        public HomePage HomePage { get; set; }
        public GuestWindow(User user)
        {
            InitializeComponent();
        
            this.User = user;
            HomePage = new HomePage(User, Main, this);
            Main.Content = HomePage;
            Main.DataContext = this;
            SetTheme(new Uri("/Styles/GuestUIdictionaryLight.xaml", UriKind.Relative));
            CurrentLanguage = "en-US";

            SwitchLanguageCommand = new GuestICommand(OnSwitchLanguage);
            BackCommand = new GuestICommand(OnBack);
            NavigationService = Main.NavigationService;
            
            DataContext = this;

        }

       
        private void OnBack()
        {

           
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
          
            


        }

     

        private void OnSwitchLanguage()
        {
          
            var app = (App)System.Windows.Application.Current;
          
            if (CurrentLanguage.Equals("en-US"))
            {
               
                CurrentLanguage = "sr-LATN";
            }
            else
            {
              
                CurrentLanguage = "en-US";
            }
            app.ChangeLanguage(CurrentLanguage);
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage(User, Main, this);
            
        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfilePage(User, Main);
            BackCommand.RaiseCanExecuteChanged();

            backButton.Visibility = Visibility.Visible;
        }

        private void AccommodationsClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccommodationsPage(User, Main);
            backButton.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            NotificationFrame.Visibility = Visibility.Visible;
            NotificationFrame.Content = new NotificationPopUp(User, Main, this);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
           NotificationFrame.Visibility = Visibility.Collapsed;
        }

        private void ThemeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            SetTheme(new Uri("/Styles/GuestUIdictionaryDark.xaml", UriKind.Relative));
        }

        private void ThemeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SetTheme(new Uri("/Styles/GuestUIdictionaryLight.xaml", UriKind.Relative));
        }

        private void SetTheme(Uri themeUri)
        {

            for (int i = 0; i < System.Windows.Application.Current.Resources.MergedDictionaries.Count; i++)
            {
                if (System.Windows.Application.Current.Resources.MergedDictionaries[i].Source.ToString() == "/Styles/GuestUIdictionaryDark.xaml" || System.Windows.Application.Current.Resources.MergedDictionaries[i].Source.ToString() == "/Styles/GuestUIdictionaryLight.xaml")
                {

                    System.Windows.Application.Current.Resources.MergedDictionaries.Remove(System.Windows.Application.Current.Resources.MergedDictionaries[i]);
                   
                
                }
            }

            
            var dict = new ResourceDictionary { Source = themeUri };
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(dict);
      
          
            
        }
        private void ForumsClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new AllForumsPage(User, Main);
            backButton.Visibility = Visibility.Visible;
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new AboutPage();
            backButton.Visibility = Visibility.Visible;

        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new HelpPage();
            backButton.Visibility = Visibility.Visible;
        }

       

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}