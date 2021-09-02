using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IGetContractByIdValidator : IValidationService<GetContractByIdRequest>
    {

    }

    internal sealed class GetContractByIdValidator : FluentValidationService<GetContractByIdRequest>, IGetContractByIdValidator
    {
        public GetContractByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-103.1");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithErrorCode("BRL-103.2");
        }
    }
}
