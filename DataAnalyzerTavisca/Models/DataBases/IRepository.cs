using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalyzerTavisca.Models.DataBases
{
    interface IRepository
    {
        string GetAllLocationsDatabase();
        string LocationWithDatesDatabases(QueryFormat query);

        string SupplierNamesWithDatesDatabase(QueryFormat query);
        string HotelNameWithDatesDatabases(QueryFormat query);
        string FailureCountDataBase(QueryFormat query);
        string PaymentDetailsDatabase(QueryFormat query);
    }
}
