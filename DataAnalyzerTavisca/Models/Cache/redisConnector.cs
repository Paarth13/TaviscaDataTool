using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using System.Web;

namespace DataAnalyzerTavisca.Models.Cache
{
    public class redisConnector
    {
        static redisConnector()
        {
            redisConnector.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost");
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}