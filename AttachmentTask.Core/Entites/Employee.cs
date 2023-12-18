
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AttachmentTask.Core.Entites
{
    public class Employee : BaseEntity
    {
        public string ?  FirstName { get; set; }
        public string  ?LastName { get; set; }
        public string ? Email { get; set; }
        public decimal ? Salary {  get; set; } 
        public string ? Phone { get; set; }
        public string ? EmployeeName { get; set; }
        public DateTime  ?HireDate { get; set; }
        public string ? Address { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();


    }
}
