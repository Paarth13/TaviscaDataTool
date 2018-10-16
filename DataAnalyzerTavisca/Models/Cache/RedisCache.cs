﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;
using DataAnalyzerTavisca.Models.DataBases;

namespace DataAnalyzerTavisca.Models.Cache
{
    public class RedisCache : ICache
    {
        redisConnector connector = new redisConnector();

        public string FailureCountCache(QueryFormat query)
        {
            IDatabase databaseRedis = connector.Connection.GetDatabase();
            string result = null;
            string data = "FailureCount";
            result = databaseRedis.StringGet(data);
            if (result == null)
            {
                IRepository sql = new SqlDatabase();

                result = sql.FailureCountDataBase(query);

                databaseRedis.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string GetAllLocationsCache()
        {
            IDatabase databaseRedis = connector.Connection.GetDatabase();
            string result = null;
            string data = "AllLocations";
            result=databaseRedis.StringGet(data);
            if(result==null)
            {
                IRepository sql = new SqlDatabase();
               
                result = sql.GetAllLocationsDatabase();
               
                databaseRedis.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string HotelNameWithDatesCache(QueryFormat query)
        {
            IDatabase databaseRedis = connector.Connection.GetDatabase();
            string result = null;
            string data = query.ToDate + query.Filter+query.FromDate;
            result = databaseRedis.StringGet(data);
            if (result == null)
            {
                IRepository sql = new SqlDatabase();

                result = sql.HotelNameWithDatesDatabases(query);

                databaseRedis.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string LocationWithDatesCache(QueryFormat query)
        {
            IDatabase databaseRedis = connector.Connection.GetDatabase();
            string result = null;
            string data =query.ToDate+query.FromDate;
            result = databaseRedis.StringGet(data);
            if (result == null)
            {
                IRepository sql = new SqlDatabase();

                result = sql.LocationWithDatesDatabases(query);

                databaseRedis.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string PaymentDetailsCache(QueryFormat query)
        {
            IDatabase databaseRedis = connector.Connection.GetDatabase();
            string result = null;
            string data = query.ToDate + query.FromDate+"Payment"+query.Filter;
            result = databaseRedis.StringGet(data);
            if (result == null)
            {
                IRepository sql = new SqlDatabase();

                result = sql.PaymentDetailsDatabase(query);

                databaseRedis.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }

        public string SupplierNamesWithDatesCache(QueryFormat query)
        {
            IDatabase databaseRedis = connector.Connection.GetDatabase();
            string result = null;
            string data = query.ToDate + query.FromDate+query.Filter;
            result = databaseRedis.StringGet(data);
            if (result == null)
            {
                IRepository sql = new SqlDatabase();

                result = sql.SupplierNamesWithDatesDatabase(query);

                databaseRedis.StringSet(data, result, TimeSpan.FromMinutes(1));
            }
            return result;
        }
    }
}