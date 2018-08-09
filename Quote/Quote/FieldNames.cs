using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
    public class FieldNames
    {
        public List<Results> result { get; set; }

  

    }

    public class Results
    {
        public List<WorkSheets> worksheets { get; set; }
    }

    public class WorkSheets
    {
        public string name { get; set; }
        public List<Columns> columns { get; set; }
    }

    public class Columns
    {
        public string name { get; set; }
        public Rows rows { get; set; }
    }

    public class Rows
    {
        public string make { get; set; }
        public string model { get; set; }

    }
}
