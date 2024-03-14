using BookingApp.Model;
using BookingApp.Repository;
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
                        TouristWindow touristWindow = new TouristWindow();
                        touristWindow.Show();
                    }
                    else if (user.Type.ToString().Equals("host"))
                    {
                        HostWindow hostWindow = new HostWindow();
                        hostWindow.Show();
                    }
                    else if (user.Type.ToString().Equals("guide"))
                    {
                        NewTourWindow newTourWindow = new NewTourWindow();
                        newTourWindow.Show();
                    }

                    else if (user.Type.ToString().Equals("guest"))
                    {
                        GuestWindow guestWindow = new GuestWindow(user);
                        guestWindow.ShowDialog();
                    }
                    //commentsOverview.Show();
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
