using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarApi.Controllers
{
    [Route("event")]
    public class SingleEventController : UltricoController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var query = new Queries.SingleEventQueries.Get(id);
            var result = ServiceActorRefs.CalendarQueryActor.Ask(query).GetAwaiter().GetResult();
            return Ok(result);
        }
                
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] ScheduleEventModel scheduleEventModel)
        {
            var command = new Commands.SingleEventCommands.Add(scheduleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> EditEvent([FromQuery]int eventId, [FromBody] ScheduleEventModel scheduleEventModel)
        {
            var command = new Commands.SingleEventCommands.Update(eventId, scheduleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteSingleEvent(int id)
        {
            var command = new Commands.SingleEventCommands.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}