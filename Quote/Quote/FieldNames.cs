using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
    public class FieldNames
    {
        [JsonProperty("Result")]
        public List<Results> Result { get; set; }
    }

    public class Results
    {
        [JsonProperty("worksheets")]
        public List<WorkSheets> worksheets { get; set; }
        public string Id { get; set; }
        public string AlternativeName { get; set; }
        public string Name { get; set; }
    }

    public class WorkSheets
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("xlsheet")]
        public xlsheet xlsheet { get; set; }

    }

    public class xlsheet
    {
        [JsonProperty("columns")]
        public List<Columns> columns { get; set; }
    }

    public class Columns
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("rows")]
        public List<string> rows { get; set; }
    }
}