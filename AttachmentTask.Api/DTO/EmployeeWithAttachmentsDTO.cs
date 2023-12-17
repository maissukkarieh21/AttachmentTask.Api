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
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string EmployeeName { get; set; }
        public DateTime HireDate { get; set; }
        public string? Address { get; set; }
        //public string Name { get; set; }
        public string Description { get; set; }
        //public IFormFile FileData { get; set; }
        public List<IFormFile> Files { get; set; }
        public DateTime Date { get; set; }
    }
}
