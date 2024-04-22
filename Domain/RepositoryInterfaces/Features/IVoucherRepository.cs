using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();
        void Add(Voucher voucher);
        int NextId();
        List<Voucher> FindVouchersByUser(int id);
        Voucher? SetVoucherToUsed(int id);
        void RefreshVouchers();
    }
}
