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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _service;

        public TicketsController(ITicketsService service)
        {
            _service = service;
        }

        [HttpPost("sale")]
        [ValidateJson("sale")]
        [RequestSizeLimit(2024)]
        public async Task<ActionResult> Sale(SegmentInputModel inputModel)
        {
            await _service.AddTicket(inputModel);
            return Ok();
        }

        [HttpPost("refund")]
        [ValidateJson("refund")]
        [RequestSizeLimit(2024)]
        public async Task<ActionResult> Refund(RefundInputModel inputModel)
        {
            await _service.RefundTicket(inputModel);
            return Ok();
        }
    }
}