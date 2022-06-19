using System;
using System.ComponentModel.DataAnnotations;

namespace TicketsApp.InputModels
{
    public class Route
    {
        [Required]
        public string AirlineCode { get; set; }
        [Required]
        public int FlightNum { get; set; }
        [Required]
        public string DepartPlace { get; set; }
        [Required]
        public DateTimeOffset DepartDatetime { get; set; }
        [Required]
        public string ArrivePlace { get; set; }
        [Required]
        public DateTimeOffset ArriveDatetime { get; set; }
        [Required]
        public string PnrId { get; set; }
    }
}