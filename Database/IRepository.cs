using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IRepository
    {
        string GetAllLocationsDatabase();
        string LocationWithDatesDatabases(QueryFormat query);

        string SupplierNamesWithDatesDatabase(QueryFormat query);
        string HotelNameWithDatesDatabases(QueryFormat query);
        string BookingDatesDatabase(QueryFormat query);
        string FailureCountDataBase(QueryFormat query);
        string PaymentDetailsDatabase(QueryFormat query);

        string TotalHotelBookingsDataBase();
    }
}
