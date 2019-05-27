using System;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    
    [Route("EventFromSeries")]
    public class EventFromSeriesController : UltricoController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditedEventFromSeries(int id)
        {
            var query = new Queries.EditEventFromSeriesQueries.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }
        
        [HttpPost("editEventFromSeries/{seriesId}")]
        public async Task<IActionResult> EditEventFromSeries([FromBody] ScheduleEventModelFromSeries scheduleEventModel, int seriesId)
        {
            var command = new Commands.EditEventFromSeriesCommands.EditEventFromSeries(scheduleEventModel, seriesId);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpDelete("{seriesId}")]
        public async Task<IActionResult> DeleteEventFromSeries(int seriesId, [FromQuery] DateTime dateTime)
        {
            var command = new Commands.EditEventFromSeriesCommands.DeleteEventFromSeries(seriesId, dateTime);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}