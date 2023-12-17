using AttachmentTask.Api.DTO;
using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using AttachmentTask.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AttachmentTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public AttachmentController(IAttachmentRepository attachmentRepository, IEmployeeRepository employeeRepository)
        {
            _attachmentRepository = attachmentRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("getAttachmentsByEmployee/{employeeId}")]
        public async Task<IActionResult> GetAttachmentsByEmployee(int employeeId)
        {
            var attachments = await _attachmentRepository.GetAttachmentsByEmployeeIdAsync(employeeId);
            return Ok(attachments);
        }

        [HttpGet("getAttachmentsById/{attachmentId}")]
        public async Task<IActionResult> GetAttachmentsById(int attachmentId)
        {
            var attachments = await _attachmentRepository.GetByIdAsync(attachmentId);
            return Ok(attachments);
        }

        //[HttpPost("addAttacment")]
        //public async Task<IActionResult> AddAttachment([FromForm] AttachmentDTO attachmentDTO)
        //{
        //    try
        //    {
        //        foreach (var file in attachmentDTO.Files)
        //        {
        //            if (file.Length > 20 * 1024 * 1024)
        //            {
        //                return BadRequest("File size exceeds the maximum limit (20 MB).");
        //            }

        //            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx" };
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
        //                    Description = attachmentDTO.Description,
        //                    FileData = fileBytes,
        //                    Date = DateTime.UtcNow,
        //                    //Employee = employee
        //                };

        //                //employee.Attachments.Add(attachment);

        //                await _attachmentRepository.AddAsync(attachment);
        //            }
        //        }

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
        //    }
        //}

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost("addAttachment")]
        public async Task<IActionResult> AddAttachment([FromForm] AttachmentDTO attachmentDTO,  List<IFormFile> Files)
        {
            try
            {

                var temporaryEmployeeId = GenerateTemporaryEmployeeId();

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


                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();

                        var attachment = new Attachment
                        {
                            Name = file.FileName,
                            Description = attachmentDTO.Description,
                            FileData = fileBytes,
                            Date = DateTime.UtcNow,
                            EmpolyeeId = temporaryEmployeeId,
                        };

                        await _attachmentRepository.AddAsync(attachment);
                    }
                }

                return Ok(new { TemporaryEmployeeId = temporaryEmployeeId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }


        //[HttpPatch("associateAttachmentsWithEmployee/{employeeId}")]
        //public async Task<IActionResult> AssociateAttachmentsWithEmployee(int employeeId, [FromBody] AttachmentDTO attachmentDTO)
        //{
        //    try
        //    {
        //        var attachments = await _attachmentRepository.GetAttachmentsByTemporaryEmployeeIdAsync(attachmentDTO.EmployeeId);

        //        foreach (var attachment in attachments)
        //        {
        //            attachment.EmpolyeeId = employeeId;
        //            await _attachmentRepository.UpdateAsync(attachment);
        //        }

        //        return Ok($"Attachments associated with EmployeeId: {employeeId} successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
        //    }
        //}

        //[HttpPost("associateAttachment/{id}")]
        //public async Task<IActionResult> AssociateAttachmentWithEmployee(int id, Attachment attachment)
        //{
        //    var existingAttachment = await _attachmentRepository.GetByIdAsync(id);
        //    if (existingAttachment == null)
        //    {
        //        return NotFound();
        //    }

        //    var existingEmployee = await _employeeRepository.GetByIdAsync(id);
        //    if (existingEmployee == null)
        //    {
        //        return NotFound();
        //    }
        //    existingEmployee.Attachments.Add(attachment);
        //    await _employeeRepository.UpdateAsync(existingEmployee);

        //    return Ok(attachment);
        //}

        private int GenerateTemporaryEmployeeId()
        {
            int temporaryEmployeeCounter = 1;
            return temporaryEmployeeCounter++;
        }
    }
}
