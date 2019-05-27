using System;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    public class CalendarController : UltricoController
    {
        [HttpGet("getEventsMetadata")]
        public async Task<IActionResult> GetEvents([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var query = new Queries.GetEventMetadata(from, to);
            var result = ServiceActorRefs.CalendarServiceActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }
    }
}
