using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using accommodationsRESTService.JsonModels;
using accommodationsRESTService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace accommodationsRESTService.Controllers
{   /// <summary>
    /// Controller which manages accommodations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InfoAboutAccommodationsController : ControllerBase
    {
        private readonly string pathToAccommodationsFile;

        private readonly AllAccommodations allAccommodations;

        private readonly ILogger _logger;

        public InfoAboutAccommodationsController(IConfiguration config, IWebHostEnvironment env, ILogger<InfoAboutAccommodationsController> logger)
        {
            pathToAccommodationsFile = config.GetValue<string>("AppSettings:PathToAccommodationsFile");
            var accommodationsJsonString = System.IO.File.ReadAllText(env.ContentRootPath + pathToAccommodationsFile);
            allAccommodations = JsonSerializer.Deserialize<AllAccommodations>(accommodationsJsonString);
            _logger = logger;
        }

        /// <summary>
        /// This action checks all accommodations and calculates the cheapest accommodation for certain month
        /// </summary>
        /// <returns>List of months with cheapest accommodation in certain month</returns>
        [HttpGet("feed")]
        [Produces("application/json")]
        public ActionResult<List<MonthWithCheapestAcc>> ListOfMonthsWithCheapestAccommodation()
        {
            try
            {
                _logger.LogInformation("GET: api/infoaboutaccommodations/feed START");
                var liMonthsWithCheapestAcc = new List<MonthWithCheapestAcc>();
                var dictMonthsWithCheapestAcc = allAccommodations.GetDictMonthsWithCheapestAcc();
       
                // Rewrite all months into the list and identify suitable accommodations with the lowest prices using the Id
                foreach (var elementInDict in dictMonthsWithCheapestAcc)
                {
                    var newMonthWithInfo = new MonthWithCheapestAcc()
                    {
                        Month = elementInDict.Key,
                        CheapestAccommodation = allAccommodations.Accommodations.Find(el => el.Id == elementInDict.Value.Item2)
                    };
                    liMonthsWithCheapestAcc.Add(newMonthWithInfo);
                }

                _logger.LogInformation("GET: api/infoaboutaccommodations/feed RETURN");

                return liMonthsWithCheapestAcc;
            }
            catch (Exception e)
            {
                _logger.LogError("GET: api/infoaboutaccommodations/feed Error occured: " + e.ToString());
                return BadRequest();
            }
        }

        /// <summary>
        /// This action takes id as an parameter and returns all prices ordered by date for the accommodation connected with this ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of InfoAboutPrice objects</returns>
        [HttpGet("prices/{id?}")]
        [Produces("application/json")]
        public ActionResult<List<InfoAboutPrice>> ListOfAllPricesForRequestedAccommodation(int id)
        {
            try
            {
                _logger.LogInformation("GET: api/infoaboutaccommodations/prices/{id} START");

                var ac = allAccommodations.Accommodations.Find(ac => ac.Id == id);
           
                if (ac != null)
                {
                    _logger.LogInformation("GET: api/infoaboutaccommodations/prices/{id} RETURN");
                    return ac.Prices.OrderBy(p => p.Date).ToList();
                }
                else
                {
                    _logger.LogInformation("GET: api/infoaboutaccommodations/prices/{id} RETURN");
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("GET: api/infoaboutaccommodations/prices/{id} Error occured: " + e.ToString());
                return BadRequest();
            }
        }
    }
}