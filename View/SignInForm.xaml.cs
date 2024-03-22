using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.GuideWindows;
using BookingApp.View.TouristWindows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;
        private readonly GuidedTourRepository _guidedTourRepository;
        private readonly TourRepository _tourRepository;
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            _guidedTourRepository = new GuidedTourRepository();
            _tourRepository = new TourRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    //CommentsOverview commentsOverview = new CommentsOverview(user);
                    //MessageBox.Show(user.Type.ToString()); //spram ovog napraviti pozivanje novih prozora
                    if (user.Type.ToString().Equals("tourist"))
                    {
                        TouristWindow touristWindow = new TouristWindow(user.Username);
                        touristWindow.Show();
                    }
                    else if (user.Type.ToString().Equals("host"))
                    {
                        HostWindow hostWindow = new HostWindow();
                        hostWindow.Show();
                    }
                    else if (user.Type.ToString().Equals("guide"))
                    {
                        if(_guidedTourRepository.HasTourCurrently(user.Id)) {
                            Tour tour = _tourRepository.GetTourById(_guidedTourRepository.FindTourIdByGuide(user.Id));
                            GuideWithTourWindow guideWithTourWindow = new GuideWithTourWindow(new DTO.TourDTO(tour), user);
                            guideWithTourWindow.Show();
                        }
                        else
                        {
                            GuideMainWindow guideMainWindow = new GuideMainWindow(user);
                            guideMainWindow.Show();
                        }

                    }

                    else if (user.Type.ToString().Equals("guest"))
                    {
                        GuestWindow guestWindow = new GuestWindow(user);
                        guestWindow.ShowDialog();
                    }
                    Close();

                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

            
        }
    }
}
