using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApp.InputModels
{
    public class SegmentInputModel
    {
        [Required]
        public string OperationType { get; set; }
        [Required]
        public DateTimeOffset OperationTime { get; set; }
        [Required]
        public string OperationPlace { get; set; }
        public Passenger Passenger { get; set; }

        public ICollection<Route> Routes { get; set; }
    }
}