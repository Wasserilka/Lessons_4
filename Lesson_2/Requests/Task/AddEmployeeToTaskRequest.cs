namespace Timesheets.Requests
{
    public class AddEmployeeToTaskRequest
    {
        public long EmployeeId { get; set; }
        public long TaskId { get; set; }
    }
}
