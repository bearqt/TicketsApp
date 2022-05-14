using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var jsonInput = ReadJsonInput(context);

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>(); 

            var jsonValidator = new JsonSchemaValidator();
            var schemaPath = _operation == "sale" ? configuration["JsonSchemaPaths:Sale"] :
                                    _operation == "refund" ? configuration["JsonSchemaPaths:Refund"] : default;
            if (!jsonValidator.Validate(schemaPath, jsonInput))
            {
                context.HttpContext.Response.StatusCode = 400;
            }
        }

        private static string ReadJsonInput(ActionExecutingContext context)
        {
            var jsonInput = "";
            var req = context.HttpContext.Request;
            req.Body.Position = 0;
            req.EnableBuffering();
            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 2048, true))
            {
                jsonInput = reader.ReadToEnd();
            }
            req.Body.Position = 0;
            return jsonInput;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}