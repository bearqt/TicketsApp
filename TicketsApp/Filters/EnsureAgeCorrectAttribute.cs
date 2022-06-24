using System;
using System.ComponentModel.DataAnnotations;

namespace TicketsApp.Filters
{
    public class EnsureAgeCorrectAttribute : ValidationAttribute
    {
        private const int MaxAge = 100;
        public override bool IsValid(object obj)
        {
            var dateTime = (DateTime) obj;
            var passengerAge = DateTime.Now.Year - dateTime.Year;
            return dateTime < DateTime.Now && passengerAge < MaxAge;
        }
    }
}