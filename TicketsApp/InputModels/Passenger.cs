using System;

namespace TicketsApp.InputModels
{
    public class Passenger
    {
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
    }
}