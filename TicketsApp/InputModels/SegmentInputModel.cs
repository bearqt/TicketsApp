using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApp.InputModels
{
    public class SegmentInputModel
    {
        
        public string OperationType { get; set; }
        public DateTimeOffset OperationTime { get; set; }
        public string OperationPlace { get; set; }
        public Passenger Passenger { get; set; }

        public ICollection<Route> Routes { get; set; }
    }
}