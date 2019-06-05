using System;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoApiCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    /// <summary>
    ///     Controller used to get events metadata needed for frontend display.
    /// </summary>
    public class CalendarController : UltricoController
    {
        /// <summary>
        ///     Gets all events metadata from certain period of time to show on calendar frontend.
        /// </summary>
        /// <param name="from">
        ///     Start DateTime of period to look for events.
        /// </param>
        /// <param name="to">
        ///     End DateTime of period to look for events.
        /// </param>
        /// <returns>
        ///     EventMetadata structure with times, durations and titles of events.
        /// </returns>
        [HttpGet("getEventsMetadata")]
        [ProducesResponseType(typeof(EventMetadata), 200)]
        public IActionResult GetEvents([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var query = new Queries.GetEventMetadata(from, to);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }
    }
}