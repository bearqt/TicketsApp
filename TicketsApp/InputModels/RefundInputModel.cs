using System;

namespace TicketsApp.InputModels
{
    public class RefundInputModel
    {
        public string OperationType { get; set; }
        public DateTimeOffset OperationTime { get; set; }
        public string OperationPlace { get; set; }
        public long TicketNumber { get; set; }
    }
}