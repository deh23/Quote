using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
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
            fileParser("data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64, SGVsbG8gV29ybGQ=", json);
            string s = "";
        }

        public void fileParser(string base64EncodedData, string json)
        {

            var obj = JsonConvert.DeserializeObject(json);
            var jsonObj = JObject.Parse(json);
         //   DataApi2.Quote quote = new DataApi2.Quote();
            JObject test = JObject.FromObject(json);
            var data = base64EncodedData.Split(",");
            base64EncodedData = data[1];
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            var decodedBytes = System.Text.ASCIIEncoding.ASCII.GetString(base64EncodedBytes);


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
}
