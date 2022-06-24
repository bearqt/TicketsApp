using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsApp.Data.Models;
using TicketsApp.InputModels;

namespace TicketsApp.Data.Services.TicketsServices
{
    public interface ITicketsService
    {
        Task AddTicketAsync(SegmentInputModel inputModel);
        Task RefundTicketAsync(RefundInputModel inputModel);
    }
}