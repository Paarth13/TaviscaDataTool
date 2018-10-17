using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaviscaDataAnalyzerDatabase.Models;

namespace TaviscaDataAnalyzerServiceProvider
{
    public interface IWebApiServiceProvider
    {
        string GetAllLocationsCache();
        string HotelsAtALocationWithDatesCache(QueryFormat query);
        string HotelNameWithDatesCache(QueryFormat query);

        string SupplierNamesWithDatesCache(QueryFormat query);
        string FailureCountCache(QueryFormat query);
        string PaymentDetailsCache(QueryFormat query);
        string BookingDatesCache(QueryFormat query);
        string TotalHotelBookingsCache();
    }
}
