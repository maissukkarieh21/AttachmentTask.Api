using AttachmentTask.Api.DTO;
using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using AttachmentTask.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace AttachmentTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IAttachmentRepository attachmentRepository, IEmployeeRepository employeeRepository)
        {
            _attachmentRepository = attachmentRepository;
            _employeeRepository = employeeRepository;
        }

        

        //[HttpPost("addEmployeeWithAttachments")]
        //public async Task<IActionResult> AddEmployeeWithAttachments([FromForm] EmployeeWithAttachmentsDTO employeeDTO)
        //{
        //    if (employeeDTO == null || employeeDTO.Files == null || !employeeDTO.Files.Any())
        //    {
        //        return BadRequest("Invalid data");
        //    }

        //    try
        //    {
        //        var employee = new Employee
        //        {
        //            FirstName = employeeDTO.FirstName,
        //            LastName = employeeDTO.LastName,
        //            Email = employeeDTO.Email,
        //            Phone = employeeDTO.Phone,
        //            Salary = employeeDTO.Salary,
        //            EmployeeName = employeeDTO.EmployeeName,
        //            HireDate = DateTime.UtcNow,
        //            Address = employeeDTO.Address
        //        };

        //        await _employeeRepository.AddAsync(employee);

        //        var employeeId = employee.Id;

        //        foreach (var file in employeeDTO.Files)
        //        {
        //            if (file.Length > 20 * 1024 * 1024)
        //            {
        //                return BadRequest("File size exceeds the maximum limit (20 MB).");
        //            }

        //            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif",".pdf",".docx" };
        //            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        //            if (!allowedExtensions.Contains(fileExtension))
        //            {
        //                return BadRequest("Invalid file type. Allowed types are: " + string.Join(", ", allowedExtensions));
        //            }

        //            using (var ms = new MemoryStream())
        //            {
        //                await file.CopyToAsync(ms);
        //                var fileBytes = ms.ToArray();

        //                var attachment = new Attachment
        //                {
        //                    Name = file.FileName,
        //                    FileData = fileBytes,
        //                    Date = DateTime.UtcNow,
        //                    //Employee = employee
        //                };

        //                //employee.Attachments.Add(attachment);

        //                await _attachmentRepository.AddAsync(attachment);
        //            }
        //        }

        //         await _employeeRepository.UpdateAsync(employee);

        //        return Ok(employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
        //    }
        //}

        [HttpGet("getEmployeeWithAttachments/{employeeId}")]
        public async Task<IActionResult> GetEmployeeWithAttachments(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            return Ok(employee);
        }

        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employee = await _employeeRepository.ListAllAsync();
            return Ok(employee);
        }


        


    }
}
