using TaviscaDataAnalyzerDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaviscaDataAnalyzerCache
{
    public interface ICache
    {
        void Post(string key, string value);
        string Get(string key);
    }
}
