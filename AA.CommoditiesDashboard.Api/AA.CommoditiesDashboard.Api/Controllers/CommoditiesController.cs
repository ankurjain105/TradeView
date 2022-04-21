using Microsoft.AspNetCore.Mvc;
using System;

namespace AA.CommoditiesDashboard.Api.Controllers
{
    [ApiController]
    public class CommoditiesController
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return new OkObjectResult(DateTime.UtcNow.ToString());
        }
    }
}
