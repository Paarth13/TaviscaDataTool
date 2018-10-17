using TaviscaDataAnalyzerDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaviscaDataAnalyzerDatabase
{
    public interface IRepository
    {
        string GetAllLocationsDatabase();
        string HotelsAtALocationWithDatesDatabases(QueryFormat query);

        string SupplierNamesWithDatesDatabase(QueryFormat query);
        string HotelNameWithDatesDatabases(QueryFormat query);
        string BookingDatesDatabase(QueryFormat query);
        string FailureCountDataBase(QueryFormat query);
        string PaymentDetailsDatabase(QueryFormat query);

        string TotalHotelBookingsDataBase();
    }
}
