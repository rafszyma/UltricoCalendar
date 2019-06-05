using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoApiCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    /// <summary>
    ///     CRUD operations on SingleEvent entity.
    /// </summary>
    [Route("event")]
    public class SingleEventController : UltricoController
    {
        /// <summary>
        ///     Gets SingleEventModel of given id.
        /// </summary>
        /// <param name="id">
        ///     Id of SingleEvent we want to get.
        /// </param>
        /// <returns>
        ///     Returns SingleEventModel which represents SingleEvent in database.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SingleEvent), 200)]
        public IActionResult GetEvent(int id)
        {
            var query = new Queries.EventQueries<SingleEvent>.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }

        /// <summary>
        ///     Creates SingleEvent in database.
        /// </summary>
        /// <param name="singleEventModel">
        ///     SingleEventModel which represents new SingleEvent properties.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpPost]
        public IActionResult PostEvent([FromBody] SingleEventModel singleEventModel)
        {
            var command = new Commands.EventCommands<SingleEventModel>.Add(singleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Edits SingleEvent of given id with new properties.
        /// </summary>
        /// <param name="id">
        ///     Id of SingleEvent to edit.
        /// </param>
        /// <param name="singleEventModel">
        ///     Model which represents new properties of SingleEvent.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpPost("edit/{id}")]
        public IActionResult EditEvent(int id, [FromBody] SingleEventModel singleEventModel)
        {
            var command = new Commands.EventCommands<SingleEventModel>.Update(id, singleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Deletes SingleEvent from database.
        /// </summary>
        /// <param name="id">
        ///     Id of SingleEvent to delete.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteSingleEvent(int id)
        {
            var command = new Commands.EventCommands<SingleEventModel>.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}