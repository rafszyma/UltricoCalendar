using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    [Route("series")]
    public class EventSeriesController : UltricoController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventSeries(int id)
        {
            var query = new Queries.EventSeriesQueries.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostEventSeries([FromBody] ScheduleEventSeries scheduleEventSeries)
        {
            var command = new Commands.EventSeriesCommands.Add(scheduleEventSeries);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> EditSeries([FromQuery]int eventId, [FromBody] ScheduleEventSeries scheduleEvent)
        {
            var command = new Commands.EventSeriesCommands.Update(eventId, scheduleEvent);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteEventSeries(int id)
        {
            var command = new Commands.EventSeriesCommands.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}