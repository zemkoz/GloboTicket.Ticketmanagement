using FluentValidation.Results;

namespace GloboTicket.TicketManagement.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValidationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
