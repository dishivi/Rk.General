namespace Rk.Web.Api.Models
{
    public class Employee : BaseRequest
    {
        public Guid EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int EmployeeAge { get; set; }
    }
}
