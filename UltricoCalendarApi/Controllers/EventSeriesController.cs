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
        public async Task<IActionResult> PostEventSeries([FromBody] ScheduleEventModelSeries scheduleEventModelSeries)
        {
            var command = new Commands.EventSeriesCommands.Add(scheduleEventModelSeries);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditSeries(int id, [FromBody] ScheduleEventModelSeries scheduleEventModel)
        {
            var command = new Commands.EventSeriesCommands.Update(id, scheduleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventSeries(int id)
        {
            var command = new Commands.EventSeriesCommands.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}