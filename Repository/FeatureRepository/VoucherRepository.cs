using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class VoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = GetAll();
        }

        public List<Voucher> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public int NextId()
        {
            _vouchers = GetAll();
            if (_vouchers.Count < 1)
                return 1;
            return _vouchers.Max(t => t.Id) + 1;
        }

        public List<Voucher> FindVouchersByUser(int id)
        {
           return _vouchers.FindAll(v => v.TouristId == id && !v.HasBeenUsed);
        }

        public Voucher? SetVoucherToUsed(int id)
        {
            Voucher voucher = _vouchers.Find(v => v.Id == id);
            if (voucher == null)
            {
                return null;
            }

            voucher.HasBeenUsed = true;
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;

        }
    }
}
