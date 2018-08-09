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
            Console.WriteLine("Hello World!");
            Program test = new Program();
            test.DeansTest();
        }
        public void DeansTest()
        {
            var json = @"{


    ""worksheets"": [
	{
		""name"" : ""xlsheet1"",
		""columns"" : [
			{
				""name"" : ""A"",
				""rows"" : {
					""fluke"" : ""sdgdsg"",
					""agliant"" : ""egdshgfh"",
					""tektronix"": """"	}
},
			{
				""name"" : ""B"",
				""rows"" : {
					""beagle"" : ""dsgdsgds"",
					""row2data"" : """",
					""tektronix"": """" }
			},
			{
				""name"" : ""c"",
				""rows"" : {
					""fluker"" : ""kjhkjh"",
					""row3data"" : """",
					""tektronix"": """"	}
			}
		]

	},
		{
		""name"" : ""xlsheet2"",
		""columns"" : [
			{
				""name"" : ""A2"",
				""rows"" : {
					""make"" : ""ghkhj"",
					""fluke"" : """",
					""tektronix"": """"	}
			},
			{
				""name"" : ""B2"",
				""rows"" : {
					""make"" : """",
					""row2data2"" : """",
					""tektronix"": """" }
			},
			{
				""name"" : ""c2"",
				""rows"" : {
					""make"" : """",
					""row3data2"" : """",
					""tektronix"": """"	}
			}
		]
		
	},
			{
		""name"" : ""xlsheet3"",
		""columns"" : [
			{
				""name"" : ""A3"",
				""rows"" : {
					""make"" : """",
					""fluke"" : """",
					""tektronix"": """"	}
			},
			{
				""name"" : ""B3"",
				""rows"" : {
					""make"" : """",
					""row2data2"" : """",
					""tektronix"": """" }
			},
			{
				""name"" : ""c3"",
				""rows"" : {
					""make"" : """",
					""row3data2"" : """",
					""tektronix"": """"	}
			}
		]
		
	}
]
}";

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
                  ""Make"",
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
            fileParser("data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64, SGVsbG8gV29ybGQ=", json, jsonTest);
            string s = "";
        }

        public void fileParser(string base64EncodedData, string json, string jsonTest)
        {
            //     var obj = JsonConvert.DeserializeObject(json);

            //  DataApi2.Quote quote = new DataApi2.Quote();
            // var data = base64EncodedData.Split(",");
            //  base64EncodedData = data[1];
            //   var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            //   var decodedBytes = System.Text.ASCIIEncoding.ASCII.GetString(base64EncodedBytes); 
            //JToken result;
            JToken result;
            JArray jArray;
         //   JObject jsonObj = JObject.Parse(json);
            JObject jsonObj = JObject.Parse(jsonTest);
            var children = jsonObj.Children();
            var valueList = new List<string>();
       //     var quoting = JsonConvert.DeserializeObject<FieldNames>(json);
            var quoting = JsonConvert.DeserializeObject<FieldNames>(jsonTest);
            //foreach (var sheet in quoting)
            //{

            //    foreach (var column in sheet.columns)
            //    {
            //        Console.WriteLine("");
            //    }
            //}

            Console.WriteLine("");



            foreach (var child in children)
            {
                Console.WriteLine("");
            }


            //    // valuesList.AddRange(child["values"].ToObject<List<string>>());
            //    var valuesJsonArray = JsonConvert.SerializeObject(valueList); // not sure if you want an array of strings or a json array of strings
            //    //if (jsonObj.TryGetValue("Result", out JToken prefix))
            //    if (jsonObj.TryGetValue(FieldNames.Quote.Result, out result))
            //    {
            //        //    result = prefix;
            //        // FIRST ARRAY = WORKSHEET / SECOND ARRAY = ROW / 3RD COLULMN AND THE INDEX IS 0 BASED
            //        // EXCEL IS INDEX 1 STARTING ROW.
            //        jArray = (JArray)jsonObj[FieldNames.Quote.Result][0][FieldNames.Quote.worksheet];
            //        // jArray = (JArray)jsonObj[FieldNames.Quote.Result];
            //        foreach (var j in jArray)


            //            //              string[] valuesArray = j.ToObject<string[]>();
            //            Console.WriteLine("");
            //    }
            //    Console.WriteLine(result[0][FieldNames.Quote.worksheet]);
            //    foreach (var child in result[0][FieldNames.Quote.worksheet])
            //    {
            //        //  jArray = (JArray)child["name"];

            //        Console.WriteLine(child);
            //        Console.WriteLine("");
            //    }
            //    for (var i = 0; i < result[0][FieldNames.Quote.worksheet].Count(); i++)
            //    {

            //        Console.WriteLine("data: " + result[0][FieldNames.Quote.worksheet][i]);
            //    }
            //    Console.WriteLine("");
            //}
            //          Console.WriteLine(((JArray)result)[0]["Id"]);
            string test = "";

        }
    }
}
//    static async Task ReadObjectDataAsync()
//    {
//        var responseBody = "";
//        try
//        {
//            GetObjectRequest request = new GetObjectRequest
//            {
//                BucketName = "com.keysight.ironbridge",
//                Key = "Upload_Documents/Fixture/quoteheader/serialAsset.xlsx"
//            };
//            AmazonS3Client client = new AmazonS3Client(RegionEndpoint.GetBySystemName("eu-west-2"));
//            using (GetObjectResponse response = await client.GetObjectAsync(request))
//            using (Stream responseStream = response.ResponseStream)
//            using (StreamReader reader = new StreamReader(responseStream))
//            {
//                var id = Path.GetFileNameWithoutExtension(response.Key);
//                string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
//                string contentType = response.Headers["Content-Type"];
//                Console.WriteLine("Object metadata, Title: {0}", title);
//                Console.WriteLine("Content type: {0}", contentType);

//                responseBody = reader.ReadToEnd(); // Now you process the response body.
//                Debug.WriteLine(reader.ReadToEnd());
//                //  string  result = IOUtils.toString(response);

//                string s = "";
//            }
//        }
//        catch (AmazonS3Exception e)
//        {
//            Console.WriteLine("Error encountered ***. Message:'{0}' when writing an object", e.Message);
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
//        }
//    }
//}