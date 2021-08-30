using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IGetInvoiceByIdValidator : IValidationService<GetInvoiceByIdRequest>
    {

    }

    internal sealed class GetInvoiceByIdValidator : FluentValidationService<GetInvoiceByIdRequest>, IGetInvoiceByIdValidator
    {
        public GetInvoiceByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-122.1");
        }
    }
}
