using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoApiCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    /// <summary>
    ///     RUD operations on EventsFromSeries.
    /// </summary>
    [Route("eventFromSeries")]
    public class EventFromSeriesController : UltricoController
    {
        /// <summary>
        ///     Gets single EventFromSeries model from database.
        /// </summary>
        /// <param name="id">
        ///     Id number of EventFromSeries we want to get.
        /// </param>
        /// <returns>
        ///     EventFromSeriesModel which represents EventFromSeries entity from database.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventFromSeries), 200)]
        public IActionResult GetEventFromSeries(int id)
        {
            var query = new Queries.EventQueries<EventFromSeries>.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }

        /// <summary>
        ///     Edits single EventFromSeries in database.
        /// </summary>
        /// <param name="id">
        ///     Id number of EventFromSeries we want to edit with new properties.
        /// </param>
        /// <param name="eventFromSeriesModel">
        ///     New model which will be overwriting old properties.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpPost("edit/{id}")]
        public IActionResult EditEventFromSeries(int id,
            [FromBody] EventFromSeriesModel eventFromSeriesModel)
        {
            var command = new Commands.EventCommands<EventFromSeriesModel>.Update(id, eventFromSeriesModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Deletes single EventFromSeries in database.
        /// </summary>
        /// <param name="id">
        ///     Id number of EventFromSeries we want to delete from database.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteEventFromSeries(int id)
        {
            var command = new Commands.EventCommands<EventFromSeriesModel>.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}