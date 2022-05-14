using NJsonSchema;

namespace TicketsApp.JsonSchemaValidators
{
    public class JsonSchemaValidator : IJsonValidator
    {
        public bool Validate(string schemaPath, string json)
        {
            var schema = JsonSchema.FromFileAsync(schemaPath);
            var errors = schema.Result.Validate(json);
            return errors.Count == 0;
        }
    }
}