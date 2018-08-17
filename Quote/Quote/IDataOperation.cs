using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    public interface IDataOperation
    {
        Task<JObject> GetPagingDataByType(string type, int pageNumber = 0, int pageSize = 50);
        Task<JObject> GetById(string type, string id);
        Task<JObject> Post(string type, JObject value, bool overwrite = false);
        Task<JObject> Put(string type, string id, JObject value);
        Task<JObject> Delete(string type, string id);
        Task<JObject> SearchPagingData(string type, JObject condition, int pageNumber = 0, int pageSize = 50);
        Task<JObject> SearchAnyString(string type, string anystring, int pageNumber = 0, int pageSize = 50);
    }
}
