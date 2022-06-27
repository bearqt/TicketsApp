using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Data.Models;
using TicketsApp.Data.Services.TicketValidators;
using TicketsApp.InputModels;

namespace TicketsApp.Data.Services.TicketsServices
{
    public class TicketsService : ITicketsService
    {
        private readonly TicketsDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITicketValidator _validator;

        public TicketsService(TicketsDbContext context, IMapper mapper, ITicketValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }
        
        public async Task AddTicketAsync(SegmentInputModel inputModel)
        {
            const int maxTimeout = 12_000; // ms
            
            if (!_validator.Validate(inputModel))
            {
                throw new BadHttpRequestException("Invalid input value(s)");
            }
            int count = 0;
            foreach (var route in inputModel.Routes)
            {
                var source1 = _mapper.Map<SegmentInputModel, Segment>(inputModel);
                var newSegment = _mapper.Map<Route, Segment>(route, source1);
                newSegment.SerialNumber = ++count;
                await _context.Segments.AddAsync(newSegment);
            }
            
            var source = new CancellationTokenSource();
            source.CancelAfter(maxTimeout);
            await _context.SaveChangesAsync(source.Token);
        }

        public async Task RefundTicketAsync(RefundInputModel inputModel)
        {
            var rowsChanged = await _context.Database.ExecuteSqlRawAsync(
                $"UPDATE \"Segments\" SET \"OperationType\" = '{inputModel.OperationType}'," +
                $"\"OperationPlace\" = '{inputModel.OperationPlace}'," +
                $"\"OperationTime\" = '{inputModel.OperationTime}'" +
                $"WHERE \"TicketNumber\" = '{inputModel.TicketNumber}' AND \"OperationType\" != 'refund';");
            if (rowsChanged == 0) throw new DbUpdateException("Ticket has already been refund or doesnt exist");
        }
    }
}