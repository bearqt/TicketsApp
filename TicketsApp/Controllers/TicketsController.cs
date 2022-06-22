using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Data;
using TicketsApp.Data.Models;
using TicketsApp.Data.Services;
using TicketsApp.Filters;
using TicketsApp.InputModels;

namespace TicketsApp.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [RequestSizeLimit(2024)]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _service;

        public TicketsController(ITicketsService service)
        {
            _service = service;
        }

        [HttpPost("sale")]
        [ValidateJson("sale")]
        public async Task<ActionResult> SaleAsync(SegmentInputModel inputModel)
        {
            await _service.AddTicket(inputModel);
            return Ok();
        }

        [HttpPost("refund")]
        [ValidateJson("refund")]
        public async Task<ActionResult> RefundAsync(RefundInputModel inputModel)
        {
            await _service.RefundTicket(inputModel);
            return Ok();
        }
    }
}