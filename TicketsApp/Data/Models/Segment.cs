using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApp.Data.Models
{
    public class Segment
    {
        [Key]
        public int SegmentId { get; set; }
        public int SerialNumber { get; set; }
        public string OperationType { get; set; }
        public DateTimeOffset OperationTime { get; set; }
        public int OperationTimeTimezone { get; set; }
        public string OperationPlace { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Patronymic { get; set; }
        
        public int DocType { get; set; }
        
        public long DocNumber { get; set; }
        
        public DateTime Birthdate { get; set; }
        
        public string Gender { get; set; }
        
        public string PassengerType { get; set; }
        
        public long TicketNumber { get; set; }
       
        public int TicketType { get; set; }
       
        public string AirlineCode { get; set; }
       
        public int FlightNum { get; set; }
        
        public string DepartPlace { get; set; }
       
        public DateTimeOffset DepartDateTime { get; set; }
        public int DepartDateTimeTimezone { get; set; }
        
        public string ArrivePlace { get; set; }
        
        public DateTimeOffset ArriveDateTime { get; set; }
        public int ArriveDateTimeTimezone { get; set; }
        public string PnrId { get; set; }
    }
}