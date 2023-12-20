using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentTask.Core.Entites
{
    public class AttachmentsGroup:BaseEntity
    {
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
