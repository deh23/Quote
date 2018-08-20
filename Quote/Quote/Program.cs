using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Quote
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Program test = new Program();
            test.DeansTest().Wait();
        }
        public async Task DeansTest()
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
                  ""Winnersh"",
                  ""f7ayful4cm"",
                  ""jqrfuh3bx8"",
                  ""6i1u0mvbn"",
                  ""8d3bonf8cr""
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
            await FileParser("", jsonTest);
        }
        public async Task FileParser(string base64EncodedData, string jsonTest)
        {
            QuoteLine quoteLine;
            JToken parsedResults;
            JObject jsonObj = JObject.Parse(jsonTest);
            FieldNames quoting;
            string result;
            string responseString;
            int parseCounter;
            Dictionary<string, string> test;
            Quote quote;

            quoting = JsonConvert.DeserializeObject<FieldNames>(jsonTest);
            foreach (var sheet in quoting.Result[0].worksheets)
            {
                //  Console.WriteLine(sheet.name);
                foreach (var columns in sheet.xlsheet.columns)
                {
                    foreach (var rows in columns.rows)
                    {
                        //responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8/search/category:product AND " + rows);
                        responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8//search/category:product AND f");

                        result = responseString;
                        //     test = test.Add(result);
                        if (!JObject.Parse(result).TryGetValue("Result", out parsedResults))                        
                            return;

                        if (!parsedResults.HasValues) continue; 
                        //     test = JsonConvert.DeserializeObject<Dictionary<string, Results>>(result);
                        quoteLine = JsonConvert.DeserializeObject<QuoteLine>(parsedResults.ToString());
                        parseCounter = quoteLine.Result.Count;

                        // SAFE GUARD IF IT RETURNS NO RESLUTS.
                        
                        switch(parseCounter)
                        {
                            case 1:

                                quoteLine.quote.SerialNumber = quoteLine.Result[0].Name_S;
                                break;
                            default:
                                
                                throw new Exception();
                        }
             
                    }
                    //return Ok();
                    //       await dynamo.Save("quoteline", id, JObject.FromObject(quoteLine));

                }
            }
        }
    }
}
    