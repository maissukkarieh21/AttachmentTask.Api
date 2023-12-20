using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentTask.Api.DTO
{
    public class AttachmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public byte[]? FileData { get; set; }
    }
}
