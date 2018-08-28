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
using IronBridge.DataApi.FunctionTest;
using System.IO;
using Amazon.Lambda;
using Amazon;
using Amazon.Lambda.Model;
using Prism.Events;

namespace Quote
{
    class Program
    {
        public HttpClient client = new HttpClient();
        public static IEnvironmentConfiguration config = new MemoryEnvironmentConfiguration();
        public LambdaOperation lambda = new LambdaOperation(config);
        public IPersistentStorage dynamo = new DynamoPersistentStorage(config);
        static void Main(string[] args)
        {
            Program test = new Program();
            test.DeansTest().Wait();
        }

        public InvokeResponse Invoke(string functionName,string path, string data)
        {
            JObject pathParams;
           var template_name = Path.Combine(System.Environment.CurrentDirectory, "WhooshRequestTemplate.json");
            
          var  fileData = File.ReadAllText(template_name);
            JObject payLoad = JObject.Parse(fileData);
            payLoad.Add("path", path);
            pathParams = (JObject)payLoad["pathParameters"];
            pathParams.Add("key", data);

            var region_name = config.AwsRegionName;
            var env_id = "v8";
            // var env_id = config.AwsNamespace;

            AmazonLambdaClient client = new AmazonLambdaClient(RegionEndpoint.GetBySystemName(region_name));
            InvokeRequest request = new InvokeRequest()
            {
                FunctionName = $"PY-{env_id}",
                Payload = payLoad.ToString(),
            };

            var response = client.InvokeAsync(request).Result;
            return response;
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
                  ""Google Inc."",
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
            await FileParser(jsonTest, "3ha5f49s");
        }
        public async Task FileParser(string excelJson, string s3ID)
        {
            List<Search> searchResult = new List<Search>();
            List<QuoteLine> quoteLine = new List<QuoteLine>();
            FieldNames quoting = new FieldNames();
            JToken body;
            JToken s3Body;
            JObject result;
            QuoteLine qtLine;
            JObject pathParams;
            string template_name;
            string fileData;

            quoting = JsonConvert.DeserializeObject<FieldNames>(excelJson);
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

                        var response = Invoke("v8", $"/search/{rows}/1/10/fix", rows);

                        using (StreamReader reader = new StreamReader(response.Payload))
                        {
                            var content = reader.ReadToEnd();
                            result = JObject.Parse(content);
                            body = result.GetValue("body");
                        }
                        //do not push each update to dynamo
                        //do it in batches of 100.
                        var s3Response = Invoke("v8", $"/data/s3attachment/{s3ID}/1/10/fix", s3ID);

                        using (StreamReader reader = new StreamReader(s3Response.Payload))
                        {
                            var content = reader.ReadToEnd();
                            result = JObject.Parse(content);
                            s3Body = result.GetValue("body");
                        }
                        //NestedToFlatStorage storage = new NestedToFlatStorage();
                        //storage.GetById("");
                      //  dataOperation.GetById("s3attachement", "3ha5f49s");
                        if (JObject.Parse(s3Body.ToString()).TryGetValue("Result", out JToken s3ParsedResults))
                        {
                            string testetesg = "";
                        }



                        if (JObject.Parse(body.ToString()).TryGetValue("Result", out JToken parsedResults))
                        {
                            searchResult = JsonConvert.DeserializeObject<List<Search>>(parsedResults.ToString());
                            if (searchResult.Count == 1)
                            {
                                qtLine = new QuoteLine(searchResult, QuoteLine.StatusEnum.Match)
                                {
                                    Header = "45cxbcaw",
                                    Product = "4wchgrae",
                                    Manufactuerer = "35t93uq5"
                                };
                            }
                            else if (searchResult.Count > 1)
                            {
                                qtLine = new QuoteLine(searchResult, QuoteLine.StatusEnum.Conflict)
                                {
                                    Header = "45cxbcaw",
                                    Product = "4wchgrae",
                                    Manufactuerer = "35t93uq5"
                                };
                            }
                            else
                            {
                                qtLine = new QuoteLine(searchResult, QuoteLine.StatusEnum.Ignore)
                                {
                                    Header = "45cxbcaw",
                                    Product = "4wchgrae",
                                    Manufactuerer = "35t93uq5"
                                };
                            }
                            quoteLine.Add(qtLine);
                        }
                        string CheckQuoteLine = "";

                        //}
                        //    var ndl = new DynamoList<QuoteLine>();
                        //   var newp = new Product();
                        //    ndl.Add(qtLine, QuoteLine.DynamoLogic);
                        //     quoteLine.Add(qtLine);
                        //   dynamo.Save("quoteline", JObject.FromObject(qtLine));
                        //SEND AN UPDATE TO THE QUOTE HEADER WITH A PERCENTAGE OF HOW MUCH IS DONE.
                    }
                    //return Ok();

                }
            }
            string testHowManyQuoteLinesExist = "";
        }
    }


}
