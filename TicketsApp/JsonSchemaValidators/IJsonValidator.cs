using System.Threading.Tasks;

namespace TicketsApp.JsonSchemaValidators
{
    public interface IJsonValidator
    {
        Task<bool> Validate(string schemaPath, string json);
    }
}