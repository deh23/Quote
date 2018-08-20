using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{



    public class QuoteLine
    {
        public List<Company> Result { get; set; }
        public Quote quote { get; set; }
    }
    public class Quote
    {
        public JObject Header { get; set; }
        public Company Manufactuerer { get; set; }
        public JObject Product { get; set; }
        public JObject asset { get; set; }
        public string AssetNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }
    public class Company
    {
       public enum CompanyEnum
        {
            Manufacturer,
            Supplier,
            Customer
        }
        public string Name_S { get; set; }
        public string Sites_Country_Code_S { get; set; }
        public string Sites_Name_S { get; set; }
        public string PostCode_S { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public CompanyEnum CompanyType { get; set; } 


    }

}
