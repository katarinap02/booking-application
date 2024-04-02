using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class GuideRateDTO : INotifyPropertyChanged
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
                if( _guideId != value)
                {
                    _guideId = value;
                    OnPropertyChanged(nameof(GuideId));
                }
            }
        }

        private int _knowledge;
        public int Knowledge
        {
            get
            {
                return _knowledge;
            }
            set
            {
                if(_knowledge != value)
                {
                    _knowledge = value;
                    OnPropertyChanged(nameof(Knowledge));
                }
            }
        }

        private int _language;
        public int Language
        {
            get
            {
                return _language;
            }
            set
            {
                if( _language != value)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        private int _tourInterest;
        public int TourInterest
        {
            get
            {
                return _tourInterest;
            }
            set
            {
                if(_tourInterest != value)
                {
                    _tourInterest = value;
                    OnPropertyChanged(nameof(TourInterest));
                }
            }
        }

        private string _additionalComment;
        public string AdditionalComment
        {
            get
            {
                return _additionalComment;
            }
            set
            {
                if(_additionalComment != value)
                {
                    _additionalComment = value;
                    OnPropertyChanged(nameof(AdditionalComment));
                }
            }
        }

        private List<string> _pictures;
        public List<string> Pictures
        {
            get
            {
                return _pictures;
            }
            set
            {
                if( _pictures != value)
                {
                    _pictures = value;
                    OnPropertyChanged(nameof(Pictures));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuideRateDTO() { }
        public GuideRateDTO(GuideRate guideRate)
        {
            Id = guideRate.Id;
            TouristId = guideRate.TouristId;
            GuideId = guideRate.GuideId;
            Knowledge = guideRate.Knowledge;
            Language = guideRate.Language;
            TourInterest = guideRate.TourInterest;
            AdditionalComment = guideRate.AdditionalComment;
            Pictures = guideRate.Pictures;
        }

        public GuideRate toGuideRate()
        {
            GuideRate guideRate = new GuideRate(_id, _touristId, _guideId, _knowledge, _language, _tourInterest, _additionalComment, _pictures);
            return guideRate;
        }
    }
}
