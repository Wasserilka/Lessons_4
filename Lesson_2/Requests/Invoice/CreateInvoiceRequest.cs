namespace Timesheets.Requests
{
    public class CreateInvoiceRequest
    {
        public long ContractId { get; set; }

        public long TaskId { get; set; }
    }
}
