using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class VoucherViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public VoucherViewModel() { }
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
