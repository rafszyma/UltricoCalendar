using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UltricoApiCommon.Validators
{
    public class ValidationResultModel
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys.SelectMany(
                    key => modelState[key].Errors
                        .Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }

        public List<ValidationError> Errors { get; }
    }
}