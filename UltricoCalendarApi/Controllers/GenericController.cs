using Microsoft.AspNetCore.Mvc;
using UltricoApiCommon;

namespace UltricoCalendarApi.Controllers
{
    public class GenericController<T> : UltricoController where T : IMyModel
    {
        [HttpGet]
        public IActionResult GetAll(T typo)
        {
            return Ok(typo);
        }
    }

    [Route("Test1")]
    public class MyFirstController : GenericController<FirstModel>
    {
        
    }

    [Route("Test2")]
    public class MySecondController : GenericController<SecondModel>
    {
        
    }


}