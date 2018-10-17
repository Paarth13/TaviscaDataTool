using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaviscaDataAnalyzerCache;
using TaviscaDataAnalyzerDatabase;
using TaviscaDataAnalyzerDatabase.Models;

namespace TaviscaDataAnalyzerServiceProvider
{
    public class WebApiServiceProvider : IWebApiServiceProvider
    {
        ICache cache;
        public WebApiServiceProvider()
        {
            cache = new TaviscaDataAnalyzerCache.ServiceProvider();
        }
        public string BookingDatesCache(QueryFormat query)
        {
            string result = null;
            string data = "BookingDates" + query.Filter + query.FromDate + query.ToDate;
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.BookingDatesDatabase(query);
                cache.Post(data, result);
            }
            return result;
        }

        public string FailureCountCache(QueryFormat query)
        {
            string result = null;
            string data = "FailureCount";
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.FailureCountDataBase(query);
                cache.Post(data, result);
            }
            return result;
        }

        public string GetAllLocationsCache()
        {
            string result = null;
            string data = "AllLocations";
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.GetAllLocationsDatabase();
                cache.Post(data, result);
            }
            return result;
        }

        public string HotelNameWithDatesCache(QueryFormat query)
        {
            string result = null;
            string data = query.ToDate + query.Filter + query.FromDate;
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.HotelNameWithDatesDatabases(query);
                cache.Post(data, result);
            }
            return result;
        }

        public string HotelsAtALocationWithDatesCache(QueryFormat query)
        {
            string result = null;
            string data = query.ToDate + query.FromDate;
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.HotelsAtALocationWithDatesDatabases(query);
                cache.Post(data, result);
            }
            return result;
        }

        public string PaymentDetailsCache(QueryFormat query)
        {
            string result = null;
            string data = query.ToDate + query.FromDate + "Payment" + query.Filter;
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.PaymentDetailsDatabase(query);
                cache.Post(data, result);
            }
            return result;
        }

        public string SupplierNamesWithDatesCache(QueryFormat query)
        {
            string result = null;
            string data = query.ToDate + query.FromDate + query.Filter;
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.SupplierNamesWithDatesDatabase(query);
                cache.Post(data, result);
            }
            return result;
        }

        public string TotalHotelBookingsCache()
        {
            string result = null;
            string data = "TotalHotelBookings";
            result = cache.Get(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();
                result = sqlDatabase.TotalHotelBookingsDataBase();
                cache.Post(data, result);
            }
            return result;
        }
    }
}
