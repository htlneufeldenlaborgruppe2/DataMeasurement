using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebInterfaceDataMeasurement.Data;

namespace WebInterfaceDataMeasurement.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };



        [HttpGet("[action]")]
        public IEnumerable<Message> AllMessages()
        {
         
            using (var context = new SqlprobeContext())
            {
                return context.Messages.Where((item) => item.Timesent != null).OrderByDescending(item => item.Timesent).ToList();
            }
        }

    }
}
