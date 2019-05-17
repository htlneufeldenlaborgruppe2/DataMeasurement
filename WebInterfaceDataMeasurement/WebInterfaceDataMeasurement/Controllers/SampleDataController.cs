using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebInterfaceDataMeasurement.Data;
using System.IO;

namespace WebInterfaceDataMeasurement.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        [HttpPost("[action]")]
        public string UploadMsg()
        {
            string json = new StreamReader(Request.Body).ReadToEnd();

            try
            {
                using (var context = new MyDatabaseContext())
                {
                    var msg= Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(json);

                    Device dev = context.Devices.FirstOrDefault((item) => item.Text == msg.DeviceId);

                    dev.Messages.Add(msg);


                    context.SaveChanges();


                    return "success";                  
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            
        }



        [HttpGet("[action]")]
        public IEnumerable<Message> AllMessages()
        {

            using (var context = new MyDatabaseContext())
            {
                return context.Messages.Where((item) => item.Timesent != null).OrderByDescending(item => item.Timesent).ToList();
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<Message> GetLineGraphData(string deviceid, int items)
        {
            using (var context = new MyDatabaseContext())
            {
                var ret = context.Messages.Where((item) => item.Timesent != null)
                    .Where((item) => item.DeviceId.ToLower() == deviceid.ToLower()).OrderByDescending(item => item.Timesent)
                    .Take(items).OrderBy(item => item.Timesent).ToList();

                return ret;
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<string> GetDeviceIDs()
        {
            using (var context = new MyDatabaseContext())
            {
                var ret = context.Messages.Where((item) => item.Timesent != null).Select((item) => item.DeviceId).Distinct().ToList();
                return ret;
            }
        }

    }
}
