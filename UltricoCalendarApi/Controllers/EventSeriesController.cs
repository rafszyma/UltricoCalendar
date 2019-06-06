using System;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoApiCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    /// <summary>
    ///     CRUD operations on EventSeries and creation of EventsFromSeries as well as deleting single occurence
    /// </summary>
    [Route("series")]
    public class EventSeriesController : UltricoController
    {
        /// <summary>
        ///     Gets event series of certain id.
        /// </summary>
        /// <param name="id">
        ///     Id of event series we want to get.
        /// </param>
        /// <returns>
        ///     Returns EventSeriesModel which represents EventSeries in database.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventSeries), 200)]
        public IActionResult GetEventSeries(int id)
        {
            var query = new Queries.EventSeriesQueries.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }

        /// <summary>
        ///     Creates new event series.
        /// </summary>
        /// <param name="eventSeriesModel">
        ///     Model of new EventSeries we want to add to database.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpPost]
        public IActionResult PostEventSeries([FromBody] EventSeriesModel eventSeriesModel)
        {
            var command = new Commands.EventSeriesCommands.Add(eventSeriesModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Edits certain EventSeries with new properties.
        /// </summary>
        /// <param name="id">
        ///     Id of event series we want to edit.
        /// </param>
        /// <param name="eventSeriesModel">
        ///     Model with new parameters of our event series.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpPost("edit/{id}")]
        public IActionResult EditSeries(int id, [FromBody] EventSeriesModel eventSeriesModel)
        {
            var command = new Commands.EventSeriesCommands.Update(id, eventSeriesModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Deletes EventSeries from database of given id.
        /// </summary>
        /// <param name="id">
        ///     Id of EventSeries to delete.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteEventSeries(int id)
        {
            var command = new Commands.EventSeriesCommands.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Excludes single event from series and create EventFromSeries model in database which can be edited separately.
        /// </summary>
        /// <param name="eventFromSeriesModel">
        ///     EventFromSeries model which specify which event we want to exclude and what new properties we want to set.
        /// </param>
        /// <param name="seriesId">
        ///     Id of series we want to exclude event from.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpPost("editEventFromSeries/{seriesId}")]
        public IActionResult ExcludeEventFromSeries([FromBody] EventFromSeriesModel eventFromSeriesModel,
            int seriesId)
        {
            var command =
                new Commands.EditEventFromSeriesCommands.ExcludeEventFromSeries(eventFromSeriesModel, seriesId);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }

        /// <summary>
        ///     Deletes single event occurence from the series.
        /// </summary>
        /// <param name="seriesId">
        ///     Id of the series we want to delete occurence from.
        /// </param>
        /// <param name="dateTime">
        ///     DateTime of certain event occurence.
        /// </param>
        /// <returns>
        ///     Returns 200.
        /// </returns>
        [HttpDelete("{seriesId}")]
        public IActionResult DeleteEventOccurenceFromSeries(int seriesId, [FromQuery] DateTime dateTime)
        {
            var command = new Commands.EditEventFromSeriesCommands.DeleteEventOccurenceFromSeries(seriesId, dateTime);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}