using BookingApp.Model;
using BookingApp.Services;
using BookingApp.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel
{
    public class VoucherViewModel : INotifyPropertyChanged
    {
        private readonly VoucherService _voucherService;

        public ObservableCollection<VoucherViewModel> Vouchers { get; set; }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private int _touristId;
        public int TouristId
        {
            get
            {
                return _touristId;
            }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(TouristId));
                }
            }
        }

        private int _guideId;
        public int GuideId
        {
            get
            {
                return _guideId;
            }
            set
            {
                if(_guideId != value)
                {
                    _guideId = value;
                    OnPropertyChanged(nameof(GuideId));
                }
            }
        }
        private bool _hasBeenUsed;
        public bool HasBeenUsed
        {
            get
            {
                return _hasBeenUsed;
            }
            set
            {
                if (_hasBeenUsed != value)
                {
                    _hasBeenUsed = value;
                    OnPropertyChanged(nameof(HasBeenUsed));
                }
            }
        }

        private string _reason;
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                    OnPropertyChanged(nameof(Reason));
                }
            }
        }

        private DateOnly _expireDate;
        public DateOnly ExpireDate
        {
            get
            {
                return _expireDate;
            }
            set
            {
                if (_expireDate != value)
                {
                    _expireDate = value;
                    OnPropertyChanged(nameof(ExpireDate));
                }
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (value != _userId)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private VoucherViewModel _selectedVoucher;
        public VoucherViewModel SelectedVoucher
        {
            get
            {
                return _selectedVoucher;
            }
            set
            {
                if(value != _selectedVoucher)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged(nameof(SelectedVoucher));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool RefreshVoucherDataGrid()
        {
            _voucherService.RefreshVouchers();
            Vouchers.Clear();
            List<VoucherViewModel> vouchers = _voucherService.FindVouchersByUser(UserId);
            foreach(var voucher in vouchers)
            {
                Vouchers.Add(voucher);
            }
            if (Vouchers.Count == 0)
                return false;
            return true;
        }


        public void Use()
        {
            if(_voucherService.SetVoucherToUsed(SelectedVoucher.Id) == null)
            {
                MessageBox.Show("Something wrong happened");
                return;
            }
            MessageBox.Show("You just used a voucher!");
        }

        public void NotificationButton()
        {
            TouristNotificationWindow touristNotificationWindow = new TouristNotificationWindow(UserId);
            touristNotificationWindow.ShowDialog();
        }

        public VoucherViewModel() {
            _voucherService = new VoucherService();
            Vouchers = new ObservableCollection<VoucherViewModel>();
        }
        public VoucherViewModel(Voucher voucher)
        {
            _id = voucher.Id;
            _touristId = voucher.TouristId;
            _guideId = voucher.GuideId;
            _hasBeenUsed = voucher.HasBeenUsed;
            _reason = voucher.Reason;
            _expireDate = voucher.ExpireDate;
        }

        public Voucher ToVoucher()
        {
            Voucher voucher = new Voucher(_id, _touristId, _guideId, _hasBeenUsed, _reason, _expireDate);
            voucher.Id = _id;
            voucher.TouristId = _touristId;
            voucher.GuideId = _guideId;
            voucher.HasBeenUsed = _hasBeenUsed;
            voucher.Reason = _reason;
            voucher.ExpireDate = _expireDate;
            return voucher;
        }
    }
}
