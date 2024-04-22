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

        public List<VoucherViewModel> ToVoucherViewModel(List<Voucher> Vouchers)
        {
            List<VoucherViewModel> VouchersViewModel = new List<VoucherViewModel>();
            foreach (Voucher voucher in Vouchers)
            {
                VouchersViewModel.Add(new VoucherViewModel(voucher));
            }
            return VouchersViewModel;
        }

        public List<VoucherViewModel> FindVouchersByUser(int id)
        {
            return ToVoucherViewModel(_voucherRepository.FindVouchersByUser(id));
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
