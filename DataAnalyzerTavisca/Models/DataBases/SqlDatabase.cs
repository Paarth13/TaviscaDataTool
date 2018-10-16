using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

using System.Web;
using DataAnalyzerTavisca.Models.ReturnClass;

namespace DataAnalyzerTavisca.Models.DataBases
{

    public class SqlDatabase : IRepository
    {
        private SqlConnection connector;
        private void Connection()
        {
            string constr = @"Data Source=54.86.216.216;Initial Catalog=qaTripDataWareHouse_Sync;User ID=readonlynewbies2018;Password=Tavisca@123";
            connector = new SqlConnection(constr);

        }
        public string GetAllLocationsDatabase()
        {
            Connection();
            Cities cities = new Cities();
            string query = $"SELECT DISTINCT(t3.City)FROM TripFolders t1 JOIN TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments t3 ON t2.Id = t3.TripProductId ;";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connector.Open();
            da.Fill(dt);
            connector.Close();
            foreach (DataRow dr in dt.Rows)
            {


                cities.City.Add(Convert.ToString(dr["City"]));

            }
            var json = JsonConvert.SerializeObject(cities);
            return json;
        }

        public string LocationWithDatesDatabases(QueryFormat query)
        {
            Connection();
            List<LocationWithDates> list = new List<LocationWithDates>();
            string statement = $"SELECT (t3.City),(t3.HotelName),Count(t3.City) as Bookings FROM TripFolders t1 JOIN TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments t3 ON t2.Id = t3.TripProductId JOIN PassengerSegments t4 ON t4.TripProductId=t2.Id where t3.StayPeriodStart between '{query.FromDate}' and '{query.ToDate}'  and t4.BookingStatus='Purchased' group by t3.HotelName,t3.city,t3.StayPeriodStart;";
            SqlCommand command = new SqlCommand(statement, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connector.Open();
            da.Fill(dt);
            connector.Close();
            foreach (DataRow dr in dt.Rows)
            {

                LocationWithDates locationWithDates = new LocationWithDates();
                Location hotelAndBookings = new Location();
                string city = Convert.ToString(dr["City"]);


                hotelAndBookings.HotelName = Convert.ToString(dr["HotelName"]);
                hotelAndBookings.Bookings = Convert.ToInt32(dr["Bookings"]);
                if (list.Exists(existingAlready => existingAlready.Place == city))
                {
                    list[list.FindIndex(existingAlready => existingAlready.Place == city)].location.Add(hotelAndBookings);
                    list[list.FindIndex(existingAlready => existingAlready.Place == city)].totalBookings += hotelAndBookings.Bookings;
                }
                else
                {
                    locationWithDates.location.Add(hotelAndBookings);
                    locationWithDates.Place = city;
                    locationWithDates.totalBookings += hotelAndBookings.Bookings;
                    list.Add(locationWithDates);
                }
            }
            var json = JsonConvert.SerializeObject(list);
            // var output = json.Replace("\"", "");

            return json;
        }

        public string HotelNameWithDatesDatabases(QueryFormat query)
        {
            Connection();
            List<HotelNamesWithBookings> list = new List<HotelNamesWithBookings>();
            string statement = $"SELECT (t3.HotelName),Count(t3.City) as Bookings FROM TripFolders     t1 JOIN    TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments  t3 ON t2.Id = t3.TripProductId JOIN PassengerSegments t4 ON t4.TripProductId=t2.Id where t3.StayPeriodStart between '{query.FromDate}' and '{query.ToDate}'  and t3.City='{query.Filter}' and t4.BookingStatus='Purchased' group by t3.HotelName,t3.StayPeriodStart ;";
            SqlCommand command = new SqlCommand(statement, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connector.Open();
            da.Fill(dt);
            connector.Close();
            foreach (DataRow dr in dt.Rows)
            {
                HotelNamesWithBookings hotelNamesWithBookings = new HotelNamesWithBookings();
                hotelNamesWithBookings.HotelName = Convert.ToString(dr["HotelName"]);
                hotelNamesWithBookings.Bookings = Convert.ToInt32(dr["Bookings"]);
                list.Add(hotelNamesWithBookings);
            }
            var json = JsonConvert.SerializeObject(list);
            return json;
        }

        public string SupplierNamesWithDatesDatabase(QueryFormat query)
        {
            Connection();
            List<IndividualSupplierBookings> list = new List<IndividualSupplierBookings>();
            string statement = $"SELECT (t3.SupplierFamily),Count(t3.City) as Bookings FROM TripFolders     t1 JOIN    TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments  t3 ON t2.Id = t3.TripProductId JOIN PassengerSegments t4 ON t4.TripProductId=t2.Id where t3.StayPeriodStart between '{query.FromDate}' and '{query.ToDate}'  and t3.City='{query.Filter}' and t4.BookingStatus='Purchased' group by t3.SupplierFamily,t3.city,t3.StayPeriodStart ;";
            SqlCommand command = new SqlCommand(statement, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connector.Open();
            da.Fill(dt);
            connector.Close();
            foreach (DataRow dr in dt.Rows)
            {
                string supplierFamilyname = Convert.ToString(dr["SupplierFamily"]);
                IndividualSupplierBookings supplierNamesWithBookings = new IndividualSupplierBookings();
                if (list.Exists(suppName => suppName.SupplierName == supplierFamilyname))
                {
                    list[list.FindIndex(suppName => suppName.SupplierName == supplierFamilyname)].Bookings += Convert.ToInt32(dr["Bookings"]);
                }
                else
                {
                    supplierNamesWithBookings.SupplierName = supplierFamilyname;
                    supplierNamesWithBookings.Bookings = Convert.ToInt32(dr["Bookings"]);
                    list.Add(supplierNamesWithBookings);
                }
            }
            var json = JsonConvert.SerializeObject(list);
            return json;
        }

        public string FailureCountDataBase(QueryFormat query)
        {
            Connection();
            FailuresInBooking failuresInBooking = new FailuresInBooking();
            string statement = $"SELECT COUNT(t3.BookingStatus) as Failure FROM HotelSegments     t1 JOIN    TripProducts t2 ON t1.TripProductId = t2.Id JOIN PassengerSegments  t3 ON t2.Id = t3.TripProductId where t2.ModifiedDate between '{query.FromDate}' and '{query.ToDate}' and t3.BookingStatus ='Planned' and t1.City='{query.Filter}';";
            SqlCommand command = new SqlCommand(statement, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connector.Open();
            da.Fill(dt);
            connector.Close();
            foreach (DataRow dr in dt.Rows)
            {
                failuresInBooking.count = Convert.ToInt32(dr["Failure"]);

            }
            var json = JsonConvert.SerializeObject(failuresInBooking);
            return json;
        }

        public string PaymentDetailsDatabase(QueryFormat query)
        {
            Connection();
            List<PaymentDetails> list = new List<PaymentDetails>();
            string statement = $"SELECT t3.PaymentType,Count(t3.PaymentType) as Bookings   FROM TripProducts t1 JOIN TripFolders t2 ON t1.TripFolderId=t2.FolderId JOIN Payments t3 ON t2.FolderId=t3.TripFolderId JOIN PassengerSegments t4 ON t1.Id=t4.TripProductId JOIN HotelSegments t5 ON t5.TripProductId = t1.Id where t5.City='{query.Filter}' and t1.ModifiedDate between  '{query.FromDate}' and '{query.ToDate}' and t4.BookingStatus='Purchased' and t1.ProductType='Hotel' group by t3.PaymentType; ";
            SqlCommand command = new SqlCommand(statement, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connector.Open();
            da.Fill(dt);
            connector.Close();
            foreach (DataRow dr in dt.Rows)
            {
                PaymentDetails paymentDetails = new PaymentDetails();
               paymentDetails.PaymentType = Convert.ToString(dr["PaymentType"]);
                paymentDetails.NumberOfBooking = Convert.ToInt32(dr["Bookings"]);
                list.Add(paymentDetails);
            }
            var json = JsonConvert.SerializeObject(list);
            return json;
        }
    }
}