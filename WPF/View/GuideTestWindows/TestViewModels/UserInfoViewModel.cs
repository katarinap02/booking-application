using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class UserInfoViewModel:ViewModelBase
    {
        #region POLJA
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private int _guideId;
        public int GuideId
        {
            get => _guideId;
            set
            {
                _guideId = value;
                OnPropertyChanged(nameof(GuideId));
            }
        }

        private double _average;
        public double Average
        {
            get => _average;
            set
            {
                _average = value;
                OnPropertyChanged(nameof(Average));
            }
        }

        private int _tourNumber;
        public int TourNumber
        {
            get => _tourNumber;
            set
            {
                _tourNumber = value;
                OnPropertyChanged(nameof(TourNumber));
            }
        }

        private string _mostUsedLanguage;
        public string MostUsedLanguage
        {
            get => _mostUsedLanguage;
            set
            {
                _mostUsedLanguage = value;
                OnPropertyChanged(nameof(MostUsedLanguage));
            }
        }

        private string _guideStatus;
        public string GuideStatus
        {
            get => _guideStatus;
            set
            {
                _guideStatus = value;
                OnPropertyChanged(nameof(GuideStatus));
            }
        }

        private string _LanguageTour;
        public string LanguageTour
        {
            get => _LanguageTour;
            set
            {
                _LanguageTour = value;
                OnPropertyChanged(nameof(LanguageTour));
            }
        }

        public ObservableCollection<string> pastBadges {  get; set; }

        #endregion

        private readonly GuideInfoService _guideInfoService;
        public MyICommand Quit { get; set; }    

        public UserInfoViewModel(int guideId) 
        {
            _guideInfoService = new GuideInfoService();
            GuideInformation guideInformation = _guideInfoService.GetByGuideId(guideId);
            GuideId = guideId;
            Name = guideInformation.Name;
            Surname = guideInformation.Surname;
            Username = guideInformation.Username;
            Age = guideInformation.Age;
            PhoneNumber = guideInformation.PhoneNumber;
            Email = guideInformation.Email;
            MostUsedLanguage = guideInformation.MostUsedLanguage;
            TourNumber = guideInformation.TourNumber;
            double avg = guideInformation.AverageGrade;
            Average = Math.Round(avg, 2);
            GuideStatus = guideInformation.Status.ToString() + " Guide";
            LanguageTour = guideInformation.LanguageByTour;
            
            Quit = new MyICommand(ExecuteQuitting);
        }
        
        public void ExecuteQuitting()
        {
            GuideQuittingWindow guideQuittingWindow = new GuideQuittingWindow(_guideId);
            guideQuittingWindow.ShowDialog();
        }
    }
}
