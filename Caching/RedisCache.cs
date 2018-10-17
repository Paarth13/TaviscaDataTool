//using Caching.Modals;
using Database;
using Database.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{

    public class RedisCache : ICache
    {
        IDatabase redisDatabase;
        RedisConnector connector = new RedisConnector();

        public RedisCache()
        {
            redisDatabase = connector.Connection.GetDatabase();
        }

        public string BookingDatesCache(QueryFormat query)
        {
            string result = null;
            string data = "BookingDates"+query.Filter+query.FromDate+query.ToDate;
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.BookingDatesDatabase(query);

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string FailureCountCache(QueryFormat query)
        {

            string result = null;
            string data = "FailureCount";
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.FailureCountDataBase(query);

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string GetAllLocationsCache()
        {
    
            string result = null;
            string data = "AllLocations";
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.GetAllLocationsDatabase();

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string HotelNameWithDatesCache(QueryFormat query)
        {

            string result = null;
            string data = query.ToDate + query.Filter + query.FromDate;
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.HotelNameWithDatesDatabases(query);

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string HotelsAtALocationWithDatesCache(QueryFormat query)
        {
            
            string result = null;
            string data = query.ToDate + query.FromDate;
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.HotelsAtALocationWithDatesDatabases(query);

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string PaymentDetailsCache(QueryFormat query)
        {
            
            string result = null;
            string data = query.ToDate + query.FromDate + "Payment" + query.Filter;
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.PaymentDetailsDatabase(query);

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string SupplierNamesWithDatesCache(QueryFormat query)
        {
            
            string result = null;
            string data = query.ToDate + query.FromDate + query.Filter;
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.SupplierNamesWithDatesDatabase(query);

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string TotalHotelBookingsCache()
        {
            string result = null;
            string data = "TotalHotelBookings";
            result = redisDatabase.StringGet(data);
            if (result == null)
            {
                IRepository sqlDatabase = new SqlDatabase();

                result = sqlDatabase.TotalHotelBookingsDataBase();

                redisDatabase.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }
    }

}
