using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    interface IGuideInformationRepository
    {
        GuideInformation GetByGuideId(int id);
    }
}
