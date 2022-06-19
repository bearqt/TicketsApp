using TicketsApp.InputModels;

namespace TicketsApp.Data.Services
{
    public interface ITicketValidator
    {
        bool Validate(SegmentInputModel inputModel);
    }
}