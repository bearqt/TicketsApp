using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Data.Models;
using TicketsApp.InputModels;

namespace TicketsApp.Data.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly TicketsDbContext _context;
        private readonly IMapper _mapper;

        public TicketsService(TicketsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task AddTicket(SegmentInputModel inputModel)
        {
            int count = 0;
            foreach (var route in inputModel.Routes)
            {
                var source1 = _mapper.Map<SegmentInputModel, Segment>(inputModel);
                var newSegment = _mapper.Map<Route, Segment>(route, source1);
                newSegment.SerialNumber = ++count;
                await _context.Segments.AddAsync(newSegment);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RefundTicket(RefundInputModel inputModel)
        {
            var segments = await _context.Segments.Where(s => s.TicketNumber == inputModel.TicketNumber && s.OperationType != "refund").ToListAsync();

            foreach (var s in segments)
            {
                s.OperationType = inputModel.OperationType;
                s.OperationTime = inputModel.OperationTime;
                s.OperationPlace = inputModel.OperationPlace;
            }
            
            var rowsChanged = await _context.SaveChangesAsync();
            if (rowsChanged == 0) throw new DbUpdateException();
        }
    }
}