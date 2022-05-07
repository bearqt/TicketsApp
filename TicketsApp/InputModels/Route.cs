using System;

namespace TicketsApp.InputModels
{
    public class Route
    {
        public string AirlineCode { get; set; }
        public int FlightNum { get; set; }
        public string DepartPlace { get; set; }
        public DateTimeOffset DepartDatetime { get; set; }
        public string ArrivePlace { get; set; }
        public DateTimeOffset ArriveDatetime { get; set; }
        public string PnrId { get; set; }
    }
}