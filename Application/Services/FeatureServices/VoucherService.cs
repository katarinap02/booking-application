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
    }
}
