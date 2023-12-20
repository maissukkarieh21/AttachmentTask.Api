using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentTask.Api.DTO
{
    public class EmployeeWithAttachmentsDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public decimal Salary { get; set; }
        public string? Phone { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public int AttachmentsGroupId { get; set; }
    }
}
