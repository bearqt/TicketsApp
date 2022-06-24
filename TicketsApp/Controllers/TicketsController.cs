using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Data;
using TicketsApp.Data.Models;
using TicketsApp.Data.Services.TicketsServices;
using TicketsApp.Filters;
using TicketsApp.InputModels;

namespace TicketsApp.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [RequestSizeLimit(2048)]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _service;

        public TicketsController(ITicketsService service)
        {
            _service = service;
        }

        [HttpPost("sale")]
        [ValidateJson("sale")]
        // [JsonSchemaResourceFilter("sale")]
        public async Task<ActionResult> SaleAsync(SegmentInputModel inputModel)
        {
            await _service.AddTicketAsync(inputModel);
            return Ok();
        }

        [HttpPost("refund")]
        [ValidateJson("refund")]
        public async Task<ActionResult> RefundAsync(RefundInputModel inputModel)
        {
            await _service.RefundTicketAsync(inputModel);
            return Ok();
        }
    }
}