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
    // TODO: GOOD: I like how clean the controllers are! Nice!
    // TODO: MEDIUM: I'm contemplating the need for this controller and a separate resource for events from series.
    // I would consider having this as part of events or series. 
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
        // TODO: LOW : Redundant EventFromSeries in method name - could be Get instead. Same for other methods. We know 
        // in which class we currently are. Callers should name their instances correctly (if there are any explicit callers)
        // which will read like eventFromSeries.Get(1).
        public IActionResult GetEventFromSeries(int id)
        {
            var query = new Queries.EventFromSeriesQueries.Get(id);
            // TODO: HIGH Not sure I like the static reference here. Why is the actor reference not a dependency supplied in the CTOR?
            // This: a) hides dependencies b) is not testable (unless new references are assigned to static props)
            // How would we deal with this if we need to return anything other than HTTP 200 and have the solution testable? 
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
        // TODO: HIGH : don't see any value in having edit/ here. We can PUT/PATCH /eventFromSeries/{id} and get the same effect?
        // This is more natural. If we would stick with edit here I would prefer to see it on all operations (same aas twitter API has)
        // for consistency.
        [HttpPost("edit/{id}")]
        // TODO: LOW : IMO model is enough for parameter name. We know the context. Explicit naming doesn't improve readability.
        public IActionResult EditEventFromSeries(int id,
            [FromBody] EventFromSeriesModel eventFromSeriesModel)
        {
            var command = new Commands.EventFromSeriesCommands.Update(id, eventFromSeriesModel);
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
            var command = new Commands.EventFromSeriesCommands.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}