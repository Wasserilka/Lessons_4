using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface ICreateEmployeeValidator : IValidationService<CreateEmployeeRequest>
    {

    }

    internal sealed class CreateEmployeeValidator : FluentValidationService<CreateEmployeeRequest>, ICreateEmployeeValidator
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 20)
                .WithErrorCode("BRL-120.1");
        }
    }
}
