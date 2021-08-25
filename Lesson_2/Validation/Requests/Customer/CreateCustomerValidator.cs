using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface ICreateCustomerValidator : IValidationService<CreateCustomerRequest>
    {

    }

    internal sealed class CreateCustomerValidator : FluentValidationService<CreateCustomerRequest>, ICreateCustomerValidator
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 20)
                .WithErrorCode("BRL-110.1");
        }
    }
}
