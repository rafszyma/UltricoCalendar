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
        public async Task<IActionResult> PostEvent([FromBody] SingleEventModel singleEventModel)
        {
            var command = new Commands.SingleEventCommands.Add(singleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditEvent(int id, [FromBody] SingleEventModel singleEventModel)
        {
            var command = new Commands.SingleEventCommands.Update(id, singleEventModel);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingleEvent(int id)
        {
            var command = new Commands.SingleEventCommands.Delete(id);
            ServiceActorRefs.CalendarServiceActor.Tell(command);
            return Ok();
        }
    }
}