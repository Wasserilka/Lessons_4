using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IGetAllContractsValidator : IValidationService<GetAllContractsRequest>
    {

    }

    internal sealed class GetAllContractsValidator : FluentValidationService<GetAllContractsRequest>, IGetAllContractsValidator
    {
        public GetAllContractsValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithErrorCode("BRL-102.1");
        }
    }
}
