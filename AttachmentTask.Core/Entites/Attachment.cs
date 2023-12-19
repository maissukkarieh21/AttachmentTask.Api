
namespace AttachmentTask.Core.Entites
{
    public class Attachment : BaseEntity
    {
        public string ? Name { get; set; }
        public byte[] ? FileData { get; set; }
        public DateTime  Date { get; set; }
    }
}
