using Hangfire;
using HangFirePractise.Web.Models;
using HangFirePractise.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangFirePractise.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        // Create in-memory database
        private static List<Driver> _drivers = new ();

        private readonly ILogger<DriversController> _logger;

        public DriversController(ILogger<DriversController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddDriver(Driver driver)
        {
            if (ModelState.IsValid)
            {
                _drivers.Add(driver);
                
                // fire and forget job
                var jobId = BackgroundJob
                    .Enqueue<IServiceManagement>(x => x.SendEmail());

                
                
                return CreatedAtAction("GetDriver", new {driver.Id}, driver);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetDriver(Guid id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);

            if (driver is  null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpDelete]
        public IActionResult DeleteDriver(Guid id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);

            if (driver is  null)
            {
                return NotFound();
            }

            driver.Status = 0;

            return NoContent();
        }
    }
}