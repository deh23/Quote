using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
   public class QuoteLine
    {
        public List<Result> Result { get; set; }
        public Quote quote { get; set; }
    }
    public class Quote
    {
        public JObject Header { get; set; }
        public string Manufactuerer { get; set; }
        public JObject Product { get; set; }
        public JObject asset { get; set; }
        public string AssetNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }
    public class Result
    {
      
        public string Name_S { get; set; }
        public string Sites_Country_Code_S { get; set; }
        public string Sites_Name_S { get; set; }
        public string PostCode_S { get; set; }
        public string category { get; set; }
        public string content { get; set; }
        public string link { get; set; }
        public string summary { get; set; }
    }

}
