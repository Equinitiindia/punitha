using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Punitha_WebAPI.Controllers
{
    public class MainController : ApiController
    {
        public string Get()
        {
            return "Welcome To Web API";
        }
        [HttpGet]
        [Route("GetApplications")]
        public IHttpActionResult Test()
        {
            List<Applications> items = new List<Applications>();

            using (StreamReader r = new StreamReader("Applicationdata.json"))
            {

                string json = r.ReadToEnd();

                items = JsonConvert.DeserializeObject<List<Applications>>(json);

            }
            return Ok(items);

        }

        [HttpGet]
        [Route("Update")]
        public IHttpActionResult Edit(Applications apdata)
        {
            List<Applications> items = new List<Applications>();

            using (StreamReader r = new StreamReader("Applicationdata.json"))
            {

                string jsons = r.ReadToEnd();

                items = JsonConvert.DeserializeObject<List<Applications>>(jsons);

            }
            for (int i = 0; i <= items.Count; i++)
            {
                if (items[i].ID == apdata.ID)
                {
                    items[i].ApplicationId = apdata.ApplicationId;
                    items[i].Type = apdata.Type;
                    items[i].Summary = apdata.Summary;
                    items[i].PostingDate = apdata.PostingDate;
                    items[i].Amount = apdata.Amount;
                    items[i].ClearedDate = apdata.ClearedDate;
                    items[i].IsCleared = apdata.IsCleared;
                }
            }

            string json = JsonConvert.SerializeObject(items.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"Applicationdata.json", json);
            return Ok("Updated Successfully");

        }


        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Add(Applications apdata)
        {
            List<Applications> items = new List<Applications>();


            using (StreamReader r = new StreamReader("Applicationdata.json"))
            {

                string jsons = r.ReadToEnd();

                items = JsonConvert.DeserializeObject<List<Applications>>(jsons);

            }
            items.Add(new Applications()
            {
                ID = apdata.ID,
                ApplicationId = apdata.ApplicationId,
                Type = apdata.Type,
                Summary = apdata.Summary,
                PostingDate = apdata.PostingDate,
                Amount = apdata.Amount,
                ClearedDate = apdata.ClearedDate,
                IsCleared = apdata.IsCleared

            });

            string json = JsonConvert.SerializeObject(items.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"Applicationdata.json", json);
            return Ok("Data Added Successfully");

        }



        [HttpGet]
        [Route("Delete")]
        public IHttpActionResult Delete(String ID)
        {
            List<Applications> items = new List<Applications>();

            using (StreamReader r = new StreamReader("Applicationdata.json"))
            {

                string jsons = r.ReadToEnd();

                items = JsonConvert.DeserializeObject<List<Applications>>(jsons);

            }

            items.Remove(items.Where(a => a.ID == ID).First());

            string json = JsonConvert.SerializeObject(items.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"Applicationdata.json", json);
            return Ok("Deleted Successfully");

        }

        public class Applications
        {
            public string ID { get; set; }
            public string ApplicationId { get; set; }
            public string Type { get; set; }
            public string Summary { get; set; }
            public string Amount { get; set; }
            public string PostingDate { get; set; }
            public bool IsCleared { get; set; }
            public string ClearedDate { get; set; }
        }
    }
}
