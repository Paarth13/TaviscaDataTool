
using Database.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class SqlDatabase:IRepository
    {
        SqlConnector sqlConnector = new SqlConnector();
        public string GetAllLocationsDatabase()
        {
            var connector = sqlConnector.ConnectionEstablisher();
            Cities cities = new Cities();
            string query = $"SELECT DISTINCT(t3.City)FROM TripFolders t1 JOIN TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments t3 ON t2.Id = t3.TripProductId ;";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connector.Open();
            dataAdapter.Fill(dataTable);
            connector.Close();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                cities.City.Add(Convert.ToString(dataRow["City"]));
            }
            var json = JsonConvert.SerializeObject(cities);
            return json;
        }

        public string LocationWithDatesDatabases(QueryFormat queryFormat)
        {
            var connector = sqlConnector.ConnectionEstablisher();
            List<LocationWithDates> list = new List<LocationWithDates>();
            string query = $"SELECT (t3.City),(t3.HotelName),Count(t3.City) as Bookings FROM TripFolders t1 JOIN TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments t3 ON t2.Id = t3.TripProductId JOIN PassengerSegments t4 ON t4.TripProductId=t2.Id where t3.StayPeriodStart between '{queryFormat.FromDate}' and '{queryFormat.ToDate}'  and t4.BookingStatus='Purchased' group by t3.HotelName,t3.city,t3.StayPeriodStart;";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connector.Open();
            dataAdapter.Fill(dataTable);
            connector.Close();
            foreach (DataRow dataRow in dataTable.Rows)
            {

                LocationWithDates locationWithDates = new LocationWithDates();
                Location hotelAndBookings = new Location();
                string city = Convert.ToString(dataRow["City"]);
                hotelAndBookings.HotelName = Convert.ToString(dataRow["HotelName"]);
                hotelAndBookings.Bookings = Convert.ToInt32(dataRow["Bookings"]);
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
            return json;
        }
        public string HotelNameWithDatesDatabases(QueryFormat queryFormat)
        {
            var connector = sqlConnector.ConnectionEstablisher();
            List<HotelNamesWithBookings> list = new List<HotelNamesWithBookings>();
            string query = $"SELECT (t3.HotelName),Count(t3.City) as Bookings FROM TripFolders t1 JOIN TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments t3 ON t2.Id = t3.TripProductId JOIN PassengerSegments t4 ON t4.TripProductId=t2.Id where t3.StayPeriodStart between '{queryFormat.FromDate}' and '{queryFormat.ToDate}'  and t3.City='{queryFormat.Filter}' and t4.BookingStatus='Purchased' group by t3.HotelName,t3.StayPeriodStart ;";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connector.Open();
            dataAdapter.Fill(dataTable);
            connector.Close();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                HotelNamesWithBookings hotelNamesWithBookings = new HotelNamesWithBookings();
                hotelNamesWithBookings.HotelName = Convert.ToString(dataRow["HotelName"]);
                hotelNamesWithBookings.Bookings = Convert.ToInt32(dataRow["Bookings"]);
                list.Add(hotelNamesWithBookings);
            }
            var json = JsonConvert.SerializeObject(list);
            return json;
        }
        public string SupplierNamesWithDatesDatabase(QueryFormat queryFormat)
        {
            var connector = sqlConnector.ConnectionEstablisher();
            List<IndividualSupplierBookings> list = new List<IndividualSupplierBookings>();
            string query = $"SELECT (t3.SupplierFamily),Count(t3.City) as Bookings FROM TripFolders t1 JOIN TripProducts t2 ON t1.FolderId = t2.TripFolderId JOIN HotelSegments t3 ON t2.Id = t3.TripProductId JOIN PassengerSegments t4 ON t4.TripProductId=t2.Id where t3.StayPeriodStart between '{queryFormat.FromDate}' and '{queryFormat.ToDate}' and t3.City='{queryFormat.Filter}' and t4.BookingStatus='Purchased' group by t3.SupplierFamily,t3.city,t3.StayPeriodStart ;";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connector.Open();
            dataAdapter.Fill(dataTable);
            connector.Close();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string supplierFamilyname = Convert.ToString(dataRow["SupplierFamily"]);
                IndividualSupplierBookings supplierNamesWithBookings = new IndividualSupplierBookings();
                if (list.Exists(suppName => suppName.SupplierName == supplierFamilyname))
                {
                    list[list.FindIndex(suppName => suppName.SupplierName == supplierFamilyname)].Bookings += Convert.ToInt32(dataRow["Bookings"]);
                }
                else
                {
                    supplierNamesWithBookings.SupplierName = supplierFamilyname;
                    supplierNamesWithBookings.Bookings = Convert.ToInt32(dataRow["Bookings"]);
                    list.Add(supplierNamesWithBookings);
                }
            }
            var json = JsonConvert.SerializeObject(list);
            return json;
        }
        public string FailureCountDataBase(QueryFormat queryFormat)
        {
            var connector = sqlConnector.ConnectionEstablisher();
            FailuresInBooking failuresInBooking = new FailuresInBooking();
            string query = $"SELECT COUNT(t3.BookingStatus) as Failure FROM HotelSegments t1 JOIN TripProducts t2 ON t1.TripProductId = t2.Id JOIN PassengerSegments  t3 ON t2.Id = t3.TripProductId where t2.ModifiedDate between '{queryFormat.FromDate}' and '{queryFormat.ToDate}' and t3.BookingStatus ='Planned' and t1.City='{queryFormat.Filter}';";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connector.Open();
            dataAdapter.Fill(dataTable);
            connector.Close();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                failuresInBooking.count = Convert.ToInt32(dataRow["Failure"]);

            }
            var json = JsonConvert.SerializeObject(failuresInBooking);
            return json;
        }

        public string PaymentDetailsDatabase(QueryFormat queryFormat)
        {
            var connector = sqlConnector.ConnectionEstablisher();
            List<PaymentDetails> list = new List<PaymentDetails>();
            string query = $"SELECT t3.PaymentType,Count(t3.PaymentType) as Bookings   FROM TripProducts t1 JOIN TripFolders t2 ON t1.TripFolderId=t2.FolderId JOIN Payments t3 ON t2.FolderId=t3.TripFolderId JOIN PassengerSegments t4 ON t1.Id=t4.TripProductId JOIN HotelSegments t5 ON t5.TripProductId = t1.Id where t5.City='{queryFormat.Filter}' and t1.ModifiedDate between  '{queryFormat.FromDate}' and '{queryFormat.ToDate}' and t4.BookingStatus='Purchased' and t1.ProductType='Hotel' group by t3.PaymentType; ";
            SqlCommand command = new SqlCommand(query, connector)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connector.Open();
            dataAdapter.Fill(dataTable);
            connector.Close();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                PaymentDetails paymentDetails = new PaymentDetails();
                paymentDetails.PaymentType = Convert.ToString(dataRow["PaymentType"]);
                paymentDetails.NumberOfBooking = Convert.ToInt32(dataRow["Bookings"]);
                list.Add(paymentDetails);
            }
            var json = JsonConvert.SerializeObject(list);
            return json;
        }
    }
}

