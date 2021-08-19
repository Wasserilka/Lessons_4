using System.Collections.Generic;

namespace Timesheets.Responses
{
    public class GetAllInvoicesResponse
    {
        public List<InvoiceDto> Invoices { get; set; }
    }
}
