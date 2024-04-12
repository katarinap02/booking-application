using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class VoucherService
    {
        private readonly VoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = new VoucherRepository();
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
    }
}
