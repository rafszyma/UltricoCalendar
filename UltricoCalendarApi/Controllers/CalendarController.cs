using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;
using UltricoCalendarContracts;

namespace UltricoCalendarApi.Controllers
{
    public class CalendarController : UltricoController
    {
        [HttpGet("hubs")]
        public async Task<IActionResult> GetAllHubs()
        {
            AkkaSystem.ActorSystem.ActorSelection("akka.tcp://ultrico-calendar@localhost:8081/user/calendar-actor").Tell(new Commands.AddEvent());
            return Ok();
        }
    }
}
