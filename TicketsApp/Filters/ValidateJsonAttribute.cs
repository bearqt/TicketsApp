using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using NJsonSchema;
using TicketsApp.JsonSchemaValidators;
using Microsoft.Extensions.DependencyInjection;

namespace TicketsApp.Filters
{
    // Filter for validation on JSON schema
    public class ValidateJsonAttribute : Attribute, IResourceFilter
    {
        private readonly string _operation;
        
        public ValidateJsonAttribute(string operation)
        {
            _operation = operation;
        }
        public async void OnResourceExecuting(ResourceExecutingContext context)
        {
            var jsonInput = await ReadJsonInput(context);
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var jsonValidator = new JsonSchemaValidator();
            var schemaPath = configuration[$"JsonSchemaPaths:{_operation}"];
            var isJsonValid = await jsonValidator.Validate(schemaPath, jsonInput);
            if (!isJsonValid)
            {
                context.Result = new BadRequestResult();
            }
        }

        private static async Task<string> ReadJsonInput(ActionContext context)
        {
            context.HttpContext.Request.Body.Position = 0;
            var jsonBody = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();
            context.HttpContext.Request.Body.Position = 0;
            return jsonBody;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }
    }
}