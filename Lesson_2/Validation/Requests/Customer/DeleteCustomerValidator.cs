using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IDeleteCustomerValidator : IValidationService<DeleteCustomerRequest>
    {

    }

    internal sealed class DeleteCustomerValidator : FluentValidationService<DeleteCustomerRequest>, IDeleteCustomerValidator
    {
        public DeleteCustomerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-111.1");
        }
    }
}
