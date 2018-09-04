using Newtonsoft.Json.Linq;
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
using System.Linq;
using System;

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
        public InvokeResponse Invoke(string functionName, string path, string data)
        {
            JObject pathParams;
            var template_name = Path.Combine(System.Environment.CurrentDirectory, "WhooshRequestTemplate.json");

            var fileData = File.ReadAllText(template_name);
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

            var response = client.InvokeAsync(request).GetAwaiter().GetResult();
            return response;
        }

        //SAVE TO DYANMO
        public async Task<string> ResultFinder(string rows)
        {
            var response = Invoke("v8", $"/search/category:company OR category:asset OR category:product AND {rows}/1/10/", rows);
            using (StreamReader reader = new StreamReader(response.Payload))
            {
                string content = await reader.ReadToEndAsync();
                return content;
            }
        }

        public async Task<IEnumerable<string>> SearchData(IEnumerable<string> rows)
        {
            var result = new List<string>();

            foreach (var row in rows)
            {
                result.Add(await ResultFinder(row));
            }

            return result;
        }

        public async Task FileParser(string excelJson, string s3ID)
        {
            List<Search> searchResult = new List<Search>();
            List<QuoteLine> quoteLine = new List<QuoteLine>();
            FieldNames quoting = new FieldNames();
            JToken body = null;
            QuoteLine qtLine;
            List<string> list = new List<string>();

            quoting = JsonConvert.DeserializeObject<FieldNames>(excelJson);

            foreach (var sheet in quoting.Result[0].worksheets)
            {
                //  Console.WriteLine(sheet.name);
                foreach (var columns in sheet.xlsheet.columns)
                {
                    foreach (var rows in columns.rows)
                    {
                        list.Add(rows);
                        // Task.Run( () => task.Add(ResultFinder(rows).ToString()));

                    }
                }
                //}
                //    var ndl = new DynamoList<QuoteLine>();
                //   var newp = new Product();
                //    ndl.Add(qtLine, QuoteLine.DynamoLogic);
                //     quoteLine.Add(qtLine);
                //   dynamo.Save("quoteline", JObject.FromObject(qtLine));
                //SEND AN UPDATE TO THE QUOTE HEADER WITH A PERCENTAGE OF HOW MUCH IS DONE.
            }
            List<Task> values = new List<Task>();

            var thread_count = 10;
            int page_size = list.Count / thread_count;
            if (page_size > 99)
            {
                page_size = 100;
            }
            var final = new List<string>();
            List<string> test = new List<string>();
            var data = new List<Task>();
            for (int i = 0; i < thread_count + 1; i++)
            {
                var to_be_processed = list.Skip(i * page_size).Take(page_size);

                var task = Task.Factory.StartNew(async () =>
                 {
                     final.AddRange(await SearchData(to_be_processed));

                     test.AddRange(to_be_processed);
                 });
                data.Add(task);
            }

            //foreach (var l in list)
            //{
            // values.Add(ResultFinder(l));
            //}
            //  Task.WaitAll(values.ToArray());
            // Task.WaitAll(final);
            await Task.WhenAll(data);

            //   await Task.WhenAll(task); // task.Wait();
            foreach (var t in final)
            {
                var content = JObject.Parse(t.ToString());
                body = content.GetValue("body");

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
                    //  }

                }
            }
            //return Ok();
            // Task.WaitAll();
            //    foreach (var r in result)
            //     {

            string tests = "";

        }
    }
}