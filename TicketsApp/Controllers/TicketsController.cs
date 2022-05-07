using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsApp.Data;
using TicketsApp.Data.Models;
using TicketsApp.Filters;
using TicketsApp.InputModels;

namespace TicketsApp.Controllers
{
    [ApiController]
    [Route("/")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsDbContext _context;

        public TicketsController(TicketsDbContext context)
        {
            _context = context;
        }

        [HttpPost("/sale")]
        [ValidateJson("sale")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Sale(SegmentInputModel inputModel)
        {
            if (HttpContext.Response.StatusCode == 400) // JSON is not valid
            {
                return StatusCode(400);
            }
            
            if (HttpContext.Response.StatusCode == 413) // JSON size > 2 Kb
            {
                return StatusCode(413);
            }

            if (_context.Segments.Any(s => s.TicketNumber == inputModel.Passenger.TicketNumber)) // Ticket is already sold
            {
                return Conflict();  
            }
            foreach (var route in inputModel.Routes)
            {
                var newSegment = new Segment()
                {
                    OperationType = inputModel.OperationType,
                    OperationTime = inputModel.OperationTime,
                    OperationPlace = inputModel.OperationPlace,
                    Name = inputModel.Passenger.Name,
                    Surname = inputModel.Passenger.Surname,
                    Patronymic = inputModel.Passenger.Patronymic,
                    DocType = inputModel.Passenger.DocType,
                    DocNumber = inputModel.Passenger.DocNumber,
                    Birthdate = inputModel.Passenger.Birthdate,
                    Gender = inputModel.Passenger.Gender,
                    PassengerType = inputModel.Passenger.PassengerType,
                    TicketNumber = inputModel.Passenger.TicketNumber,
                    TicketType = inputModel.Passenger.TicketType,
                    AirlineCode = route.AirlineCode,
                    FlightNum = route.FlightNum,
                    DepartPlace = route.DepartPlace,
                    DepartDateTime = route.DepartDatetime,
                    ArrivePlace = route.ArrivePlace,
                    ArriveDateTime = route.ArriveDatetime,
                    PnrId = route.PnrId,
                };
                _context.Add(newSegment);
            }
            
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("/refund")]
        [ValidateJson("refund")]
        public ActionResult Refund(RefundInputModel inputModel)
        {
            if (HttpContext.Response.StatusCode == 400) // JSON is not valid
            {
                return StatusCode(400);
            }
            
            var segments = _context.Segments.Where(s => s.TicketNumber == inputModel.TicketNumber);

            if (!segments.Any()) // No such segments to refund
            {
                return Conflict();
            }
            
            foreach (var s in segments)
            {
                if (s.OperationType == "refund") // segment is already refund
                {
                    return Conflict();
                }
                
                s.OperationType = inputModel.OperationType;
                s.OperationTime = inputModel.OperationTime;
                s.OperationPlace = inputModel.OperationPlace;
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}