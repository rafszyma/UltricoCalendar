using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon;

namespace UltricoCalendarApi.Controllers
{
    public class HomeController : UltricoController
    {
        [HttpGet("hubs")]
        public async Task<IActionResult> GetAllHubs()
        {
            return Ok();
        }
    }
}
