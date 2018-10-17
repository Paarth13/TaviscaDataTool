using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
   public interface ICache
    {
        string GetAllLocationsCache();
        string LocationWithDatesCache(QueryFormat query);
        string HotelNameWithDatesCache(QueryFormat query);

        string SupplierNamesWithDatesCache(QueryFormat query);
        string FailureCountCache(QueryFormat query);
        string PaymentDetailsCache(QueryFormat query);
        string BookingDatesCache(QueryFormat query);
        string TotalHotelBookingsCache();
    }
}
