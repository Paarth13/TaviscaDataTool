using TaviscaDataAnalyzerDatabase.Models.Hotels;
using TaviscaDataAnalyzerDatabase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;
using TaviscaDataAnalyzerServiceProvider;

namespace TaviscaDataAnalyzerApi.Controllers
{
    public class HotelsController : ApiController
    {

        [HttpGet]
        [Route("api/HotelLocations")]
        public object GetAllLocations()
        {
            IWebApiServiceProvider service = new WebApiServiceProvider();
            Cities ListOfCities = JsonConvert.DeserializeObject<Cities>(service.GetAllLocationsCache()); ;
            return ListOfCities;
        }

        [HttpGet]
        [Route("api/Hotels/HotelLocationWithDates")]
        public object GetHotelLocationWithDates([FromUri] string fromDate, string toDate)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate };
            IWebApiServiceProvider service = new WebApiServiceProvider();
            List<HotelsInALocationWithDates> ListOfHotelsWithDates = JsonConvert.DeserializeObject<List<HotelsInALocationWithDates>>(service.HotelsAtALocationWithDatesCache(query));
            return ListOfHotelsWithDates;
        }

        [HttpGet]
        [Route("api/Hotels/HotelNamesWithDates")]
        public object GetHotelNamesWithDates([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            IWebApiServiceProvider service = new WebApiServiceProvider();
            List<HotelNamesWithBookings> ListOfHotelNamesWithDates = JsonConvert.DeserializeObject<List<HotelNamesWithBookings>>(service.HotelNameWithDatesCache(query));
            return ListOfHotelNamesWithDates;
        }

        [HttpGet]
        [Route("api/Hotels/SupplierNamesWithDates")]
        public object GetSupplierNamesWithDates([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            IWebApiServiceProvider service = new WebApiServiceProvider();
            List<IndividualSupplierBookings> ListOfSuppliers = JsonConvert.DeserializeObject<List<IndividualSupplierBookings>>(service.SupplierNamesWithDatesCache(query));
            return ListOfSuppliers;
        }

        [HttpGet]
        [Route("api/Hotels/FailureCount")]
        public object GetFailureCount([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            IWebApiServiceProvider service = new WebApiServiceProvider();
            FailuresInBooking FailureCount = JsonConvert.DeserializeObject<FailuresInBooking>(service.FailureCountCache(query));
            return FailureCount;
        }

        [HttpGet]
        [Route("api/Hotels/PaymentType")]
        public object GetPaymentType([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            IWebApiServiceProvider service = new WebApiServiceProvider();
            List<PaymentDetails> payment = JsonConvert.DeserializeObject<List<PaymentDetails>>(service.PaymentDetailsCache(query));
            return payment;
        }

        [HttpGet]
        [Route("api/Hotels/BookingDates")]
        public object GetBookingDates([FromUri] string fromDate, string toDate, string location)
        {
            QueryFormat query = new QueryFormat { ToDate = toDate, FromDate = fromDate, Filter = location };
            IWebApiServiceProvider service = new WebApiServiceProvider();
            List<HotelBookingDates> hotelBookingDates = JsonConvert.DeserializeObject<List<HotelBookingDates>>(service.BookingDatesCache(query));
            return hotelBookingDates;
        }

        [HttpGet]
        [Route("api/Hotels/TotalBookings")]
        public object GetSuccessfulCount()
        {

            IWebApiServiceProvider service = new WebApiServiceProvider();
            List<TotalHotelBookings> totalHotelBookings = JsonConvert.DeserializeObject<List<TotalHotelBookings>>(service.TotalHotelBookingsCache());
            return totalHotelBookings;
        }

    }
}
