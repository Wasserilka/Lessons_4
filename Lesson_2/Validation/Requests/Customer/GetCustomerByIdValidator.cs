using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IGetCustomerByIdValidator : IValidationService<GetCustomerByIdRequest>
    {

    }

    internal sealed class GetCustomerByIdValidator : FluentValidationService<GetCustomerByIdRequest>, IGetCustomerByIdValidator
    {
        public GetCustomerByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-112.1");
        }
    }
}
