using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsApp.Data.Models;
using TicketsApp.InputModels;

namespace TicketsApp.Data.Services
{
    public interface ITicketsService
    {
        Task AddTicket(SegmentInputModel inputModel);
        Task RefundTicket(RefundInputModel inputModel);
    }
}