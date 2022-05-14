namespace TicketsApp.JsonSchemaValidators
{
    public interface IJsonValidator
    {
        bool Validate(string schemaPath, string json);
    }
}