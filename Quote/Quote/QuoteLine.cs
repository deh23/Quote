using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
    public class QuoteLine
    {
        public QuoteLine()
        {
            Product = new List<Product>();
        }
        public QuoteLine(List<Product> Product,StatusEnum status)
        {
            this.Product = Product;
            this.CompanyType = status;
        }
        public QuoteLine(Product Product, StatusEnum status)
        {
            Product = new List<Product>();
            this.Product.Add(Product);
            this.CompanyType = status;
        }
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
        public Product DeserialiseJson<T>(string value)
        {
            return JsonConvert.DeserializeObject<Product>(value);
        }
        public List<Product> DeserialiseJsonList<T>(string value)
        {
            return JsonConvert.DeserializeObject<List<Product>>(value);
        }
    }
    public class Search
    {
        public string DimensionsUnit_S { get; set; }
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

        public static void DynamoLogic()
        {
            Console.Out.Write("sOME lOGIC");
            //    dynamo.Save("company", JObject.FromObject(conflictLines));
        }
    }
}
