using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private static readonly TouristService _touristService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public List<Voucher> FindVouchersByUser(int id)
        {
            return _voucherRepository.FindVouchersByUser(id);
        }

        public bool HasVoucher(int userId)
        {
            return _voucherRepository.FindVouchersByUser(userId).Count() > 0;
        }

        public Voucher SetVoucherToUsed(int id)
        {
            return _voucherRepository.SetVoucherToUsed(id);
        }

        public void RefreshVouchers()
        {
            _voucherRepository.RefreshVouchers();
        }

        public void Add(Voucher voucher)
        {
            _voucherRepository.Add(voucher);
        }

        public void AwardVoucher(int userId)
        {
            Voucher newVoucher = new Voucher(0, userId, -1, false, "You have attended 5 tours in the past year", DateOnly.FromDateTime(DateTime.Now.AddMonths(6)));
            _voucherRepository.Add(newVoucher);
            _touristService.SetTouristConqueredVoucher(userId);
        }

        public bool isTouristConqueredVoucher(int touristId)
        {
            return _touristService.isTouristConqueredVoucher(touristId);
        }
    }
}
