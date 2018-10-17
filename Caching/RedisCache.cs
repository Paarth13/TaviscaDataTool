using TaviscaDataAnalyzerDatabase;
using TaviscaDataAnalyzerDatabase.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaviscaDataAnalyzerCache
{

    public class ServiceProvider : ICache
    {
        IDatabase redisDatabase;
        RedisConnector connector = new RedisConnector();

        public ServiceProvider()
        {
            redisDatabase = connector.Connection.GetDatabase();
        }

        public string Get(string key)
        {
            string value = redisDatabase.StringGet(key);
            if (string.IsNullOrEmpty(value))
                return null;
            else
                return value;
        }


        public void Post(string key, string value)
        {
            redisDatabase.StringSet(key, value, TimeSpan.FromMinutes(1));
        }


    }
}
