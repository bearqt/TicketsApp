using System;
using System.Threading.Tasks;
using NJsonSchema;

namespace TicketsApp.JsonSchemaValidators
{
    public class JsonSchemaValidator : IJsonValidator
    {
        public async Task<bool> Validate(string schemaPath, string json)
        {
            var schema = await JsonSchema.FromFileAsync(schemaPath);
            var errors = schema.Validate(json);
            return errors.Count == 0;
        }
    }
}