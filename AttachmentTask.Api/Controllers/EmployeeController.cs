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
        //    if (employeeDTO == null || employeeDTO.Attachments == null || !employeeDTO.Attachments.Any())
        //    {
        //        return BadRequest("Invalid data");
        //    }

        //    var employee = new Employee
        //    {
        //        FirstName = employeeDTO.FirstName,
        //LastName = employeeDTO.LastName,
        //Email= employeeDTO.Email,
        //Phone= employeeDTO.Phone,
        //Image = employeeDTO.Image,
        //Salary= employeeDTO.Salary,
        //EmployeeName = employeeDTO.EmployeeName,
        //HireDate = employeeDTO.HireDate,
        //Address = employeeDTO.Address
        //    };

        //    foreach (var attachmentDTO in employeeDTO.Attachments)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            await attachmentDTO.FileData.CopyToAsync(ms);
        //            var fileBytes = ms.ToArray();

        //            var attachment = new Attachment
        //            {
        //                Name = attachmentDTO.Name,
        //                Description = attachmentDTO.Description,
        //                FileData = fileBytes,
        //                Employee = employee
        //            };

        //            employee.Attachments.Add(attachment);

        //            await _attachmentRepository.AddAsync(attachment);
        //        }
        //    }

        //    // Repository responsibility: Save employee and its associated attachments to the database
        //    await _employeeRepository.AddAsync(employee);

        //    return Ok(employee.Id);
        //}

        //[HttpPost("addEmployeeWithAttachments")]
        //public async Task<IActionResult> AddEmployeeWithAttachments([FromForm] EmployeeWithAttachmentsDTO employeeDTO)
        //{
        //    if (employeeDTO == null || employeeDTO.FileData == null)
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
        //            Image = employeeDTO.Image,
        //            Salary = employeeDTO.Salary,
        //            EmployeeName = employeeDTO.EmployeeName,
        //            HireDate = employeeDTO.HireDate,
        //            Address = employeeDTO.Address
        //        };

        //        await _employeeRepository.AddAsync(employee);

        //        var employeeId = employee.Id;

        //        using (var ms = new MemoryStream())
        //        {
        //            await employeeDTO.FileData.CopyToAsync(ms);
        //            var fileBytes = ms.ToArray();

        //            var attachment = new Attachment
        //            {
        //                Name = employeeDTO.Name,
        //                Description = employeeDTO.Description,
        //                FileData = fileBytes,
        //                Date = DateTime.UtcNow,
        //                EmpolyeeId = employeeId
        //            };

        //            await _attachmentRepository.AddAsync(attachment);
        //            employee.Attachments.Add(attachment);
        //            await _employeeRepository.UpdateAsync(employee);
        //        }

        //        return Ok(employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred while processing the request.", Details = ex.ToString() });
        //    }
        //}

        [HttpPost("addEmployeeWithAttachments")]
        public async Task<IActionResult> AddEmployeeWithAttachments([FromForm] EmployeeWithAttachmentsDTO employeeDTO)
        {
            if (employeeDTO == null || employeeDTO.Files == null || !employeeDTO.Files.Any())
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var employee = new Employee
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    Email = employeeDTO.Email,
                    Phone = employeeDTO.Phone,
                    Image = employeeDTO.Image,
                    Salary = employeeDTO.Salary,
                    EmployeeName = employeeDTO.EmployeeName,
                    HireDate = DateTime.UtcNow,
                    Address = employeeDTO.Address
                };

                await _employeeRepository.AddAsync(employee);

                var employeeId = employee.Id;

                foreach (var file in employeeDTO.Files)
                {
                    if (file.Length > 20 * 1024 * 1024)
                    {
                        return BadRequest("File size exceeds the maximum limit (20 MB).");
                    }

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif",".pdf",".docx" };
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
                            Description = employeeDTO.Description,
                            FileData = fileBytes,
                            Date = DateTime.UtcNow,
                            Employee = employee
                        };

                        employee.Attachments.Add(attachment);

                        await _attachmentRepository.AddAsync(attachment);
                    }
                }

                 await _employeeRepository.UpdateAsync(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }


        //[HttpPost("addEmployee")]
        //public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        //{
        //    var employee = new Employee 
        //    {
        //        FirstName = employeeDto.FirstName,
        //        LastName = employeeDto.LastName,
        //        Email = employeeDto.Email,
        //        Phone = employeeDto.Phone,
        //        Image = employeeDto.Image,
        //        Salary = employeeDto.Salary,
        //        EmployeeName = employeeDto.EmployeeName,
        //        HireDate = DateTime.UtcNow,
        //        Address = employeeDto.Address

        //    };



        //    return Ok();
        //}

        //[HttpPost("addEmployee")]
        //public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        //{
        //    try
        //    {
        //        var employee = new Employee
        //        {
        //            FirstName = employeeDto.FirstName,
        //            LastName = employeeDto.LastName,
        //            Email = employeeDto.Email,
        //            Phone = employeeDto.Phone,
        //            Image = employeeDto.Image,
        //            Salary = employeeDto.Salary,
        //            EmployeeName = employeeDto.EmployeeName,
        //            HireDate = DateTime.UtcNow,
        //            Address = employeeDto.Address
        //        };

        //        await _employeeRepository.AddAsync(employee);

        //        // Retrieve attachments associated with the temporary identifier
        //        var attachments = await _attachmentRepository.GetAttachmentsByTemporaryEmployeeIdAsync(employeeDto.TemporaryEmployeeId);

        //        // Update each attachment with the created employee's EmployeeId
        //        foreach (var attachment in attachments)
        //        {
        //            attachment.EmployeeId = employee.Id;
        //            await _attachmentRepository.UpdateAsync(attachment);
        //        }

        //        return Ok(new { EmployeeId = employee.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
        //    }
        //}

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                    Image = employeeDto.Image,
                    Salary = employeeDto.Salary,
                    EmployeeName = employeeDto.EmployeeName,
                    HireDate = DateTime.UtcNow,
                    Address = employeeDto.Address
                };

                await _employeeRepository.AddAsync(employee);
                var attachments = await _attachmentRepository.GetAttachmentsByTemporaryEmployeeIdAsync(employeeDto.TemporaryEmployeeId);
                foreach (var attachment in attachments)
                {
                    attachment.EmpolyeeId = employee.Id;
                    await _attachmentRepository.UpdateAsync(attachment);
                }

                return Ok(new { EmployeeId = employee.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }


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
