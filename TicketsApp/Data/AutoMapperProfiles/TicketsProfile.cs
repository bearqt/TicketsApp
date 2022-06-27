using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Template;
using TicketsApp.Data.Models;
using TicketsApp.InputModels;

namespace TicketsApp.Data.AutoMapperProfiles
{
    public class TicketsProfile : Profile
    {
        public TicketsProfile()
        {
            CreateMap<SegmentInputModel, Segment>();
            CreateMap<SegmentInputModel, Segment>()
                .ForMember(p => p.Birthdate, s => s.MapFrom(s => s.Passenger.Birthdate))
                .ForMember(p => p.Gender, s => s.MapFrom(s => s.Passenger.Gender))
                .ForMember(p => p.Name, s => s.MapFrom(s => s.Passenger.Name))
                .ForMember(p => p.Patronymic, s => s.MapFrom(s => s.Passenger.Patronymic))
                .ForMember(p => p.Surname, s => s.MapFrom(s => s.Passenger.Surname))
                .ForMember(p => p.DocNumber, s => s.MapFrom(s => s.Passenger.DocNumber))
                .ForMember(p => p.DocType, s => s.MapFrom(s => s.Passenger.DocType))
                .ForMember(p => p.PassengerType, s => s.MapFrom(s => s.Passenger.PassengerType))
                .ForMember(p => p.TicketNumber, s => s.MapFrom(s => s.Passenger.TicketNumber))
                
                .ForMember(p => p.OperationTimeTimezone, s => s.MapFrom(s => s.OperationTime.Offset.Hours))

                .ForMember(p => p.TicketType, s => s.MapFrom(s => s.Passenger.TicketType));
            CreateMap<Route, Segment>()
                .ForMember(p => p.AirlineCode, s => s.MapFrom(s => s.AirlineCode))
                .ForMember(p => p.FlightNum, s => s.MapFrom(s => s.FlightNum))
                .ForMember(p => p.DepartPlace, s => s.MapFrom(s => s.DepartPlace))
                .ForMember(p => p.DepartDateTime, s => s.MapFrom(s => s.DepartDatetime))
                .ForMember(p => p.ArrivePlace, s => s.MapFrom(s => s.ArrivePlace))
                
                .ForMember(p => p.DepartDateTimeTimezone, s => s.MapFrom(s => s.DepartDatetime.Offset.Hours))
                .ForMember(p => p.ArriveDateTimeTimezone, s => s.MapFrom(s => s.ArriveDatetime.Offset.Hours))
                
                .ForMember(p => p.ArriveDateTime, s => s.MapFrom(s => s.ArriveDatetime))
                .ForMember(p => p.PnrId, s => s.MapFrom(s => s.PnrId));
        }
    }
}