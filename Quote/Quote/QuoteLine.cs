using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
    class QuoteLine
    {
        public JObject Header { get; set; }
        public string Manufactuerer { get; set; }
        public JObject Product { get; set; }
        public JObject asset { get; set; }
        public string AssetNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }
}
