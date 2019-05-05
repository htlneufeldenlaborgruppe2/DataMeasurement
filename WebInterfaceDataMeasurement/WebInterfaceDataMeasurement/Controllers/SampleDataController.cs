using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<Message> AllMessages()
        {
            Random r = new Random();
            int max = r.Next(8, 60);
            for (int i = 0; i <max; i++)
            {
                yield return new Message()
                {
                    primaryKey = i,
                    deviceid = "Device" + r.Next(5),
                    datetime = DateTime.Now,
                    dust = r.NextDouble(),
                    ldr = r.NextDouble(),
                    co2 = r.NextDouble(),
                    humidity = r.NextDouble(),
                    temp = r.NextDouble(),
                    noise = r.NextDouble(),
                };
            }
        }


        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF {
                get {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
