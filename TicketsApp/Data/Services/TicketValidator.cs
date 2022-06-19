using System;

using TicketsApp.InputModels;

namespace TicketsApp.Data.Services
{
    public class TicketValidator : ITicketValidator
    {
        private const int TicketNumberSize = 13;
        private const int DocNumberSize = 10;
        public bool Validate(SegmentInputModel inputModel)
        {
            return ValidateTicketNumber(inputModel) && ValidateGender(inputModel) && ValidateDocNumber(inputModel) && ValidateBirthday(inputModel);
        }

        private static bool ValidateTicketNumber(SegmentInputModel inputModel)
        {
            return inputModel.Passenger.TicketNumber.ToString().Length == TicketNumberSize;
        }

        private static bool ValidateGender(SegmentInputModel inputModel)
        {
            return inputModel.Passenger.Gender is "F" or "M";
        }

        private static bool ValidateDocNumber(SegmentInputModel inputModel)
        {
            if (inputModel.Passenger.DocType == 0)
            {
                return inputModel.Passenger.DocNumber.ToString().Length == DocNumberSize;
            }
            return true;
        }

        private static bool ValidateBirthday(SegmentInputModel inputModel)
        {
            return inputModel.Passenger.Birthdate < DateTime.Now;
        }
    }
}