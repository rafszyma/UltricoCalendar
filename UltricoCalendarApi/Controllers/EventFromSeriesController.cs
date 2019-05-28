using System;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    [Route("eventFromSeries")]
    public class EventFromSeriesController : UltricoController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventFromSeries(int id)
        {
            var query = new Queries.EventQueries<EventFromSeries>.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditEventFromSeries(int id,
            [FromBody] EventFromSeriesModel eventFromSeriesModel)
        {
            var command = new Commands.EventCommands<EventFromSeriesModel>.Update(id, eventFromSeriesModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventFromSeries(int id)
        {
            var command = new Commands.EventCommands<EventFromSeriesModel>.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        [HttpPost("editEventFromSeries/{seriesId}")]
        public async Task<IActionResult> ExcludeEventFromSeries([FromBody] EventFromSeriesModel eventFromSeriesModel,
            int seriesId)
        {
            var command =
                new Commands.EditEventFromSeriesCommands.ExcludeEventFromSeries(eventFromSeriesModel, seriesId);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        [HttpDelete("{seriesId}")]
        public async Task<IActionResult> DeleteEventOccurenceFromSeries(int seriesId, [FromQuery] DateTime dateTime)
        {
            var command = new Commands.EditEventFromSeriesCommands.DeleteEventOccurenceFromSeries(seriesId, dateTime);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}