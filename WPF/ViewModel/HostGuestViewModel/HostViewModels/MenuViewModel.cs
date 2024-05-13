using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private bool isMenuOpened;
    public bool IsMenuOpened
    {
        get { return isMenuOpened; }
        set
        {
            isMenuOpened = value;
            OnPropertyChanged();
        }
    }

    private bool isRatingOpened;
    public bool IsRatingOpened
    {
        get { return isRatingOpened; }
        set
        {
            isRatingOpened = value;
            OnPropertyChanged();
        }
    }

    private bool isRenovationOpened;
    public bool IsRenovationOpened
    {
        get { return isRenovationOpened; }
        set
        {
            isRenovationOpened = value;
            OnPropertyChanged();
        }
    }

        private string searchHost;
        public string SearchHost
        {
            get { return searchHost; }
            set
            {
                searchHost = value;
                OnPropertyChanged();
            }
        }

        private bool isDemo;
        public bool IsDemo
        {
            get { return isDemo; }
            set
            {
                isDemo = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel(Host host) {
            searchHost = host.Search;
            isDemo = false;
        }
    }
}
