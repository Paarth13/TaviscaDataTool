using Caching;
using Database.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;

namespace DataAnalyzerTavisca.Controllers
{
    public class HotelsController : ApiController
    {
        // GET: api/Hotels
        [HttpGet]
        [Route("api/Hotels")]
        public object GetAllHotels()
        {
            ICache cache = new RedisCache();
            Cities ListOfCities = JsonConvert.DeserializeObject<Cities>(cache.GetAllLocationsCache()); ;
            return ListOfCities;
        }

        [HttpGet]
        [Route("api/Hotels/HotelLocationWithDates")]
        public object GetHotelLocationWithDates([FromUri] string fromDate, string toDate)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate };
            ICache cache = new RedisCache();
            List<LocationWithDates> ListOfHotelsWithDates = JsonConvert.DeserializeObject<List<LocationWithDates>>(cache.LocationWithDatesCache(query));
            return ListOfHotelsWithDates;
        }

        [HttpGet]
        [Route("api/Hotels/HotelNamesWithDates")]
        public object GetHotelNamesWithDates([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            ICache cache = new RedisCache();
            List<HotelNamesWithBookings> ListOfHotelNamesWithDates = JsonConvert.DeserializeObject<List<HotelNamesWithBookings>>(cache.HotelNameWithDatesCache(query));
            return ListOfHotelNamesWithDates;
        }

        [HttpGet]
        [Route("api/Hotels/SupplierNamesWithDates")]
        public object GetSupplierNamesWithDates([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            ICache cache = new RedisCache();
            List<IndividualSupplierBookings> ListOfSuppliers = JsonConvert.DeserializeObject<List<IndividualSupplierBookings>>(cache.SupplierNamesWithDatesCache(query));
            return ListOfSuppliers;
        }

        [HttpGet]
        [Route("api/Hotels/FailureCount")]
        public object GetFailureCount([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            ICache cache = new RedisCache();
            FailuresInBooking FailureCount = JsonConvert.DeserializeObject<FailuresInBooking>(cache.FailureCountCache(query));
            return FailureCount;
        }
        [HttpGet]
        [Route("api/Hotels/PaymentType")]
        public object GetPaymentType([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            ICache cache = new RedisCache();
            List<PaymentDetails> payment   = JsonConvert.DeserializeObject<List<PaymentDetails>>(cache.PaymentDetailsCache(query));
            return payment;
        }
    }
}
