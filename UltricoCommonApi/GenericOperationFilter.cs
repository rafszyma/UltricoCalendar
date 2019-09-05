using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UltricoCalendarApi
{
public class GenericOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerDescriptor))
            {
                return;
            }
            
            var baseType = controllerDescriptor.ControllerTypeInfo.BaseType?.GetTypeInfo();
            // Get type and see if its a generic controller with a single type parameter
            if (baseType == null || (!baseType.IsGenericType && baseType.GenericTypeParameters.Length == 1))
                return;

            if (context.ApiDescription.HttpMethod != "GET" || !baseType.GetGenericArguments().Any())
            {
                return;
            }
            
            var typeParam = baseType.GetGenericArguments()[0];

            var obj = Activator.CreateInstance(typeParam, false);

            var model = (IMyModel) obj;
            if (model == null)
            {
                return;
            }
                    
            operation.Responses.Remove("200");
            operation.Responses.Add("200", new Response
            {
                Description = "Success",
                Examples = new {model}
            });
        }
    }

    public interface IMyModel
    {
    };
        
    public class FirstModel : IMyModel 
    {
        public int FirstModelInt;
    
        public string FirstModelString;
    }
    
    public class SecondModel : IMyModel
    {
        public int SecondModelInt;
    
        public string SecondModelString;
    }
}