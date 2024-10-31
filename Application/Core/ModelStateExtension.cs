using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
     public static class ModelStateExtension
    {
        public static Microsoft.AspNetCore.Mvc.ObjectResult InvalidModelStateResponse(ModelStateDictionary modelState)
            => new BadRequestObjectResult(Result<string>.Failure(-400,
                modelState.Values.First()?.Errors.First().ErrorMessage));

    }
}
