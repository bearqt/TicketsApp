using System;
using System.ComponentModel.DataAnnotations;

namespace TicketsApp.InputModels
{
    public class Passenger
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public int DocType { get; set; }
        [Required]
        public long DocNumber { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PassengerType { get; set; }
        [Required]
        public long TicketNumber { get; set; }
        [Required]
        public int TicketType { get; set; }
    }
}