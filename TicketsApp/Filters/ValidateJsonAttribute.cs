using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ValidateJsonAttribute : Attribute, IActionFilter
    {
        private readonly string _operation;
        
        public ValidateJsonAttribute(string operation)
        {
            _operation = operation;
        }
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var jsonInput = await ReadJsonInput(context);
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>(); 

            var jsonValidator = new JsonSchemaValidator();
            var schemaPath = _operation == "sale" ? configuration["JsonSchemaPaths:Sale"] :
                                    _operation == "refund" ? configuration["JsonSchemaPaths:Refund"] : default;
            var isJsonValid = await jsonValidator.Validate(schemaPath, jsonInput);
            if (!isJsonValid)
            {
                context.HttpContext.Response.StatusCode = 400;
            }
        }

        private static async Task<string> ReadJsonInput(ActionContext context)
        {
            context.HttpContext.Request.Body.Position = 0;
            var jsonBody = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();
            return jsonBody;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}