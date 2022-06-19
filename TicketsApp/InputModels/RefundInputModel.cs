using System;
using System.ComponentModel.DataAnnotations;

namespace TicketsApp.InputModels
{
    public class RefundInputModel
    {
        [Required]
        public string OperationType { get; set; }
        [Required]
        public DateTimeOffset OperationTime { get; set; }
        [Required]
        public string OperationPlace { get; set; }
        [Required]
        public long TicketNumber { get; set; }
    }
}