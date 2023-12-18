using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using Microsoft.AspNetCore.Mvc;


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


        [HttpPost("addAttachment")]
        public async Task<IActionResult> AddAttachment(List<IFormFile> Files)
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
                    var attachment = new Attachment
                    {
                        Name = file.FileName,
                        Date = DateTime.UtcNow,
                        EmpolyeeId = temporaryEmployeeId,
                    };

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        attachment.FileData = stream.ToArray();
                    }

                    await _attachmentRepository.AddAsync(attachment);
                }
                return Ok(new { TemporaryEmployeeId = temporaryEmployeeId });
        }


        private int GenerateTemporaryEmployeeId()
        {
            int temporaryEmployeeCounter = 1;
            return temporaryEmployeeCounter++;
        }
    }
}
