using System.Collections.Generic;
using System.Linq;
using dotnet.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnet.Data.Validation
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {

            IDictionary<string, string> fieldErrors = new Dictionary<string, string>();
            foreach ((string fieldName, ModelStateEntry entry) in context.ModelState.Where(ms => ms.Value.Errors.Count > 0))
            {
                fieldErrors.Add(fieldName.CamelCase(), entry.Errors.First().ErrorMessage);
            }
            throw ResponseStatusException.UnprocessableEntity(fieldErrors);
        }
    }
}