using Microsoft.AspNetCore.Mvc;
using UltricoCalendarCommon.Validators;

namespace UltricoCalendarCommon
{
    [ValidateModel]
    public abstract class UltricoController : Controller
    {
    }
}