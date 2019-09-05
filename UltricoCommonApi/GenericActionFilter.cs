using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UltricoCalendarApi
{
public class GenericActionFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            Console.WriteLine("Hey");
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerDescriptor)
            {
                var baseType = controllerDescriptor.ControllerTypeInfo.BaseType?.GetTypeInfo();
                // Get type and see if its a generic controller with a single type parameter
                if (baseType == null || (!baseType.IsGenericType && baseType.GenericTypeParameters.Length == 1))
                    return;

                if (context.ApiDescription.HttpMethod == "GET" && baseType.GetGenericArguments().Any())
                {
                    var typeParam = baseType.GetGenericArguments()[0];

                    if (typeParam == null)
                    {
                        return;
                    }

                    MyModel obj = new FirstModel();
                    if (typeParam.ReflectedType is FirstModel)
                    {
                        obj = new FirstModel();
                        ((FirstModel) (obj)).Hey1 = 1;
                        ((FirstModel) (obj)).Hey2 = "hiho";
                    }
                    
                    if (typeParam.ReflectedType is SecondModel)
                    {
                        obj = new SecondModel();
                        ((SecondModel) (obj)).Hey3 = 1;
                        ((SecondModel) (obj)).Hey4 = "hiho";
                    }
                    
                    /*// Get the schema of the generic type. In case it's not there, you will have to create a schema for that model
                    // yourself, because Swagger may not have added it, because the type was not declared on any of the models
                    string typeParamFriendlyId = typeParam.FriendlyId();

                    if (!context.SchemaRegistry.Definitions.TryGetValue(typeParamFriendlyId, out Schema typeParamSchema))
                    {
                        // Schema doesn't exist, you need to create it yourself, i.e. add properties for each property of your model.
                        // See OpenAPI/Swagger Specifications
                        typeParamSchema = context.SchemaRegistry.GetOrRegister(typeParam);

                        // add properties here, without it you won't have a model description for this type
                    }*/

                    // for any get operation for which no 200 response exist yet in the document
                    operation.Responses.Add("200", new Response
                    {
                        Description = "Success",
                        Examples = obj
                        //Schema = new Schema { Ref = typeParamFriendlyId }
                    });
                }
            }
        }
    }

public interface MyModel
{
        
};
    
public class FirstModel : MyModel 
{
    public int Hey1;

    public string Hey2;
}

public class SecondModel : MyModel
{
    public int Hey3;

    public string Hey4;
}
}