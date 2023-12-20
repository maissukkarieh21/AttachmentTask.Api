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
        private readonly IAttachmentGroupRepository _attachmentGroupRepository;
        public AttachmentController(IAttachmentRepository attachmentRepository, IEmployeeRepository employeeRepository, IAttachmentGroupRepository attachmentGroupRepository)
        {
            _attachmentRepository = attachmentRepository;
            _employeeRepository = employeeRepository;
            _attachmentGroupRepository = attachmentGroupRepository;
        }

        


        [HttpGet("getAttachment/{Id}")]
        public async Task<IActionResult> GetAttachmentById(int attachmentId)
        {
            var attachments = await _attachmentRepository.GetByIdAsync(attachmentId);
            return Ok(attachments);
        }

        

        [HttpGet("downloadAttachment/{attachmentId}")]
        public async Task<IActionResult> DownloadAttachment(int attachmentId)
        {
            try
            {
                var attachment = await _attachmentRepository.GetByIdAsync(attachmentId);

                if (attachment == null)
                {
                    return NotFound();
                }
                var fileContentResult = new FileContentResult(attachment.FileData, "application/octet-stream")
                {
                    FileDownloadName = attachment.Name
                };

                return fileContentResult;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }


        [HttpGet("attachmentIds/{groupId}")]
        public async Task<IActionResult> GetAttachmentIdsByGroupId(int groupId)
        {
            var attachmentIds = await _attachmentRepository.GetAttachmentIdsByGroupIdAsync(groupId);

            return Ok(attachmentIds);
        }

        [HttpGet("attachmentDetails/{groupId}")]
        public async Task<IActionResult> GetAttachmentDetailsByGroupId(int groupId)
        {
            var attachmentDetails = await _attachmentRepository.GetAttachmentDetailsByGroupIdAsync(groupId);

            return Ok(attachmentDetails);
        }
    }
    
}
