using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Quote
{
    class Program
    {
        static void Main(string[] args)
        {
            Program test = new Program();
            test.DeansTest();
        }
        public void DeansTest()
        {
            var jsonTest = @"{
  ""note"": ""This is a test sample JSON return for the quote mapping : CALIBRE - 285"",
  ""PageNumber"": 0,
  ""PageSize"": 15,
  ""Total"": 1,
  ""Result"": [
    {
                ""file"": {
                    ""EntityID"": ""psdfsdfsdf"",
        ""Filename"": ""zzzzzzzz.xls""
                },
      ""worksheets"": [
        {
          ""xlsheet"": {
            ""name"": ""sheet1"",
            ""columns"": [
              {
                ""name"": ""A"",
                ""rows"": [
                  ""make"",
                  ""Fluke"",
                  ""Tektronix"",
                  ""Boonton"",
                  ""Draper""
                ]
    },
              {
                ""name"": ""B"",
                ""rows"": [
                  ""Model"",
                  ""87 Mk V"",
                  ""TDS5000"",
                  ""B001-1"",
                  ""DR098""
                ]
},
              {
                ""name"": ""C"",
                ""rows"": [
                  ""Descrip"",
                  ""Digital Multimeter"",
                  ""Digital Scope"",
                  ""Loadcell"",
                  ""Torque wrench""
                ]
              },
              {
                ""name"": ""D"",
                ""rows"": [
                  ""Serial"",
                  ""fk1290871"",
                  ""317098234/001M"",
                  ""BOON-0901"",
                  ""DT00091a-12""
                ]
              },
              {
                ""name"": ""E"",
                ""rows"": [
                  ""Charge code"",
                  ""201/2"",
                  ""200/3"",
                  ""200/4"",
                  ""001/12""
                ]
              },
              {
                ""name"": ""F"",
                ""rows"": [
                  ""Ident/asset"",
                  ""YKK-14012"",
                  ""YKB-21001"",
                  ""YKB-22127"",
                  """"
                ]
              },
              {
                ""name"": ""G"",
                ""rows"": [
                  ""Qty"",
                  ""1"",
                  ""1"",
                  ""1"",
                  ""3""
                ]
              }
            ]
          }
        },
        {
          ""xlsheet"": {
            ""name"": ""newsheet_AAA"",
            ""columns"": [
              {
                ""name"": ""A"",
                ""rows"": [
                  ""Charge code"",
                  ""17025"",
                  ""42312"",
                  ""90909"",
                  ""171""
                ]
              },
              {
                ""name"": ""B"",
                ""rows"": [
                  ""Department"",
                  ""Alpha"",
                  ""Alpha"",
                  ""Delta"",
                  """"
                ]
              }
            ]
          }
        }
      ]
    }
  ]
}";
            //   ReadObjectDataAsync().Wait();
            fileParser("data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64, SGVsbG8gV29ybGQ=", jsonTest);
            string s = "";
        }

        public void fileParser(string base64EncodedData, string jsonTest)
        {
            JObject jsonObj = JObject.Parse(jsonTest);
            var quoting = JsonConvert.DeserializeObject<FieldNames>(jsonTest);

            foreach (var sheet in quoting.Result[0].worksheets)
            {
                Console.WriteLine(sheet.name);
                foreach (var columns in sheet.xlsheet.columns)
                {
                    Console.WriteLine(columns.name);
                    foreach (var rows in columns.rows)
                    {
                        Console.WriteLine(rows);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}