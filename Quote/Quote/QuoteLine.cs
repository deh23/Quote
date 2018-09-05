using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
    public class Mapped
    {
        public string Data { get; set; }
      
        public string SetColumnName(string val)
        {
           // if(val == )
            return val;
        }
        public string columnName 
          {
            get
            {
                return Data;
            }
    set
            {
                Data = SetColumnName(value);
            }
        }
    }
    public class DataMapping //: IEnumerable
    {
        public List<string> rows { get; set; }
        public List<string> columnName { get; set; }
        public List<Map> Mapping { get; set; }
        public string StartingCell { get; set; }
        public string ImportSheet { get; set; }

        //public IEnumerator GetEnumerator()
        //{
        //    return ((IEnumerable)Mapping).GetEnumerator();
        //}
    }

    public class Map
    {
        public string Column { get; set; }
        public string map { get; set; }
    }

    public class QuoteLine
    {

        public QuoteLine()
        {
            Search = new List<Search>();
        }
        public QuoteLine(List<Search> Search, StatusEnum status)
        {
            this.Search = Search;
            this.CompanyType = status;
        }
        public QuoteLine(Search Product, StatusEnum status)
        {
            this.Search = Search;
            //  this.Search.Add(Search);
            //  this.Product = Product;
            // this.Search.Add(Search);
            this.CompanyType = status;
        }
        public string Header { get; set; }
        public string Manufactuerer { get; set; }
        public string Product { get; set; }
        public List<Search> Search { get; set; }
        public string asset { get; set; }
        public string AssetNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }

        public StatusEnum CompanyType { get; set; }
        public enum StatusEnum
        {
            Ignore,
            Match,
            Conflict
        }

        public static void DynamoLogic()
        {
            Console.Out.Write("sOME lOGIC");
            //       dynamo.Save(item.Search["category"], JObject.FromObject(item));
        }
    }
    public class QuoteLines
    {
        public JObject Header { get; set; }
        public JObject Manufactuerer { get; set; }
        public List<Product> Product { get; set; }
        public JObject asset { get; set; }
        public string AssetNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }

        public StatusEnum CompanyType { get; set; }
        public enum StatusEnum
        {
            Ignore,
            Match,
            Conflict
        }
    }
    public class QuoteHelper
    {
        public Search DeserialiseJson<T>(string value)
        {
            return JsonConvert.DeserializeObject<Search>(value);
        }
        public List<Search> DeserialiseJsonList<T>(string value)
        {
            return JsonConvert.DeserializeObject<List<Search>>(value);
        }
    }
    public class Search
    {
        //   public string DimensionsUnit_S { get; set; }
        public string Name_S { get; set; }
        public string category { get; set; }
        public string content { get; set; }
        public string link { get; set; }
        public string summary { get; set; }
    }
    public class Product
    {
        public string Name { get; set; }
        public JObject Manufacturer { get; set; }
        public string Description { get; set; }
        public int RecommendedPeriodicity { get; set; }
        public JObject ProductFamily { get; set; }
        public string SiebelProductId { get; set; }
        public bool IsUnitCalibratable { get; set; }
        public double DimensionsDepth { get; set; }
        public double DimensionsHeight { get; set; }
        public double DimensionsWidth { get; set; }
        public string DimensionsUnit { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public JObject ActiveStatus { get; set; }
        public DateTime InactiveDate { get; set; }
        public DateTime EndOfSupportDate { get; set; }
        public string ProductOrSystem { get; set; }
        public JArray Options { get; set; }
        public JObject SubProducts { get; set; }
        public JObject ServiceActivities { get; set; }


    }
}
