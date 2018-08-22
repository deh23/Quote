using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using IronBridge.Data.Operation.Generic;
using IronBridge.Data.Contract;
using IronBridge.Data.Repository;
using System.Net;

namespace Quote
{
    class Program
    {
        public HttpClient client = new HttpClient();
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
                  ""PolyCust4"",
                  ""PolyCust4"",
                  ""5720A"",
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
                   S3EnvironmentConfiguration config = new S3EnvironmentConfiguration("v8");
            config.AwsDynamoTablePrefix = "V8";
            IPersistentStorage dynamo = new DynamoPersistentStorage(config);

            //  DataOperation dataOperation = new DataOperation();
            List<Product> conflictLines = new List<Product>();
            Product quoteMatch = new Product();
            List<QuoteLine> quoteLine = new List<QuoteLine>();
            List<QuoteLines> quoteLines = new List<QuoteLines>();
            JObject jsonObj = JObject.Parse(jsonTest);
            FieldNames quoting = new FieldNames();
            string responseString;
            QuoteHelper quoteHelper = new QuoteHelper();

     

            quoting = JsonConvert.DeserializeObject<FieldNames>(jsonTest);
            //  client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("x-session-id", "EASY-REMB");
            client.DefaultRequestHeaders.Add("x-session-id", "password");
            // client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            foreach (var sheet in quoting.Result[0].worksheets)
            {
                //  Console.WriteLine(sheet.name);
                foreach (var columns in sheet.xlsheet.columns)
                {
                    foreach (var rows in columns.rows)
                    {

                        //      try
                        //    {
                        //COULD BE MULITPLE MANUFACTURER CONFLICTS. 
                        //responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8/data/asset/search/" + rows);
                        // }
                        //  catch (WebException)
                        //  {
                        try
                        {
                            // responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8/data/product/az8xk2ar");

                            responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8/data/product/search/" + rows);
                        }
                        catch (WebException)
                        {
                            //  QuoteLine.CompanyType = Product.StatusEnum.Ignore;

                            //SHOVE A STATUS OF NO MATCH.
                            continue;
                        }
                        //   }
                        //   responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8/data/product/search/" + rows);
                        //  responseString = await client.GetStringAsync("https://hpjei901q9.execute-api.eu-west-2.amazonaws.com/v8//search/category:product AND f");

                        //do not push each update to dynamo
                        //do it in batches of 100.
                        if (!JObject.Parse(responseString).TryGetValue("Result", out JToken parsedResults))
                        {

                            quoteMatch = quoteHelper.DeserialiseJson<Product>(responseString.ToString());

                            quoteLine.Add(new QuoteLine
                            {
                                Product = quoteMatch,
                                CompanyType = QuoteLine.StatusEnum.Match
                            }

                      );

                            //  dynamo.Save("company", JObject.FromObject(quoteMatch));

                            continue;
                        }

                        conflictLines = quoteHelper.DeserialiseJsonList<List<Product>>(parsedResults.ToString());
                        quoteLines.Add(new QuoteLines
                        {

                            Product = conflictLines,
                            CompanyType = QuoteLines.StatusEnum.Conflict
                        });
                    //    dynamo.Save("company", JObject.FromObject(conflictLines));


                        //SEND AN UPDATE TO THE QUOTE HEADER WITH A PERCENTAGE OF HOW MUCH IS DONE.
                    }
                    //return Ok();

                }
            }
            string hello = "";
        }
        public string config()
        {
            return "";
        }
    }
}
