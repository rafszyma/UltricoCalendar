using Microsoft.AspNetCore.Mvc;
using UltricoApiCommon.Validators;

namespace UltricoApiCommon
{
    [ValidateModel]
    public abstract class UltricoController : Controller
    {
    }
}