using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using Prism.Events;
using Moq;
using IronBridge.Data.Contract;
using IronBridge.Data.Repository;
using IronBridge.Data.Operation.Generic;

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
                  ""test_1-8-18_10.04"",
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
            FileParserAsync("", jsonTest);
        }
        public void FileParserAsync(string base64EncodedData, string jsonTest)
        {
            IEventAggregator eventAggregator = new EventAggregator();
            S3EnvironmentConfiguration config = new S3EnvironmentConfiguration("v8");
          
            ISearch whooshSearch = new WhooshSearch(config);
            DynamoPersistentStorage dynamoPersistentStorage = new DynamoPersistentStorage(config);
            IEntityDefinitionRepository s3EntityDefinitionRepository = new S3EntityDefinitionRepository(config);

            IRepository cache = new RedisCacheRepository(config);
            IDataManagement data = new DataManagement(cache, dynamoPersistentStorage);
            IGuidGeneration guidGenerator = new GuidGenerator(dynamoPersistentStorage);
            
            JObject jsonObj = JObject.Parse(jsonTest);

            NestedToFlatStorage nestedToFlatStorage = new NestedToFlatStorage(data, s3EntityDefinitionRepository, guidGenerator, whooshSearch, eventAggregator);
         //   DataOperation dataOperation = new DataOperation(storage);
            

            var quoting = JsonConvert.DeserializeObject<FieldNames>(jsonTest);
            QuoteLine quoteLine;

            foreach (var sheet in quoting.Result[0].worksheets)
            {
                //  Console.WriteLine(sheet.name);
                foreach (var columns in sheet.xlsheet.columns)
                {
                    //  Console.WriteLine(columns.name);
                    foreach (var rows in columns.rows)
                    {
                        //          Task<string> guid = generator.NewGuid("quoteline");
                        //            string id = guid.Result;
                           var dynamoData = nestedToFlatStorage.SearchAnyString("company", rows, 0, 15);
                     //   JObject test = JObject.Parse(rows);
                    //   dataOperation.SearchAnyString
                     //   var dynamoData = dynamo.ScanIds("company", test, 0, 15);
                        for (var i = 0; i < 2; i++)
                        {
                            if (dynamoData.HasValues)
                            {
                              //  quoteLine = JsonConvert.DeserializeObject<QuoteLine>(dynamoData.Result.ToString());
                              
                                //      quoteLine. = "";
                               
                            //       await dynamo.Save("quoteline", id, JObject.FromObject(quoteLine));
                                //  test.Result.Name;
                                continue;
                            }

                            i = 0;
                        }

                        Console.WriteLine(rows);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}