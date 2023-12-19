using AttachmentTask.Api.DTO;
using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using AttachmentTask.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AttachmentTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsGroupController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttachmentGroupRepository _attachmentGroupRepository;
        public AttachmentsGroupController(IAttachmentRepository attachmentRepository, IEmployeeRepository employeeRepository, IAttachmentGroupRepository attachmentGroupRepository)
        {
            _attachmentRepository = attachmentRepository;
            _employeeRepository = employeeRepository;
            _attachmentGroupRepository = attachmentGroupRepository;
        }

        

        [HttpPost("uploadAttachments")]
        public async Task<IActionResult> UploadAttachments(List<IFormFile> Files)
        {
            try
            {
                var employeeAttachmentsGroup = new AttachmentsGroup();
                await _attachmentGroupRepository.AddAsync(employeeAttachmentsGroup);

                var attachmentIds = new List<int>();

                foreach (var file in Files)
                {
                    if (file.Length > 20 * 1024 * 1024)

                    {
                        return BadRequest("File size exceeds the maximum limit (20 MB).");

                    }
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx" };

                    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(fileExtension))

                    {
                        return BadRequest("Invalid file type. Allowed types are: " + string.Join(", ", allowedExtensions));
                    }

                    var attachment = new Attachment
                    {
                        Name = file.FileName,
                        Date = DateTime.UtcNow,
                    };
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        attachment.FileData = stream.ToArray();
                    }

                    employeeAttachmentsGroup?.Attachments?.Add(attachment);
                    await _attachmentRepository.AddAsync(attachment);
                    attachmentIds.Add(attachment.Id);
                }

                return Ok(new { AttachmentIds = attachmentIds, AttachmentsGroupId = employeeAttachmentsGroup.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPost("addEmployeeWithAttachments")]
        public async Task<IActionResult> AddEmployeeWithAttachments([FromBody] EmployeeWithAttachmentsDTO request)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Salary = request.Salary,
                    Phone = request.Phone,
                    EmployeeName = request.EmployeeName,
                    HireDate = request.HireDate,
                    Address = request.Address,
                    AttachmentsGroupId = request.AttachmentsGroupId,
                };

                await _employeeRepository.AddAsync(employee);

                return Ok(new { EmployeeId = employee.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }


    }
}
