using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NJsonSchema;

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
            string jsonInput = "";
            var req = context.HttpContext.Request;
            req.Body.Position = 0;
            req.EnableBuffering();
            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 2048, true))
            {
                jsonInput = reader.ReadToEnd();
            }
            req.Body.Position = 0;

            var schema = _operation == "sale" ? JsonSchema.FromFileAsync("../TicketsApp/JsonSchemas/saleSchema.json") : 
                                        _operation == "refund" ? JsonSchema.FromFileAsync("../TicketsApp/JsonSchemas/refundSchema.json") : default;
            
            var errors = schema.Result.Validate(jsonInput);
            
            if (errors.Count > 0)
            {
                context.HttpContext.Response.StatusCode = 400;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}