using TicketsApp.InputModels;

namespace TicketsApp.Data.Services.TicketValidators
{
    public interface ITicketValidator
    {
        bool Validate(SegmentInputModel inputModel);
    }
}