
namespace AttachmentTask.Core.Entites
{
    public class Attachment : BaseEntity
    {
        public string  Name { get; set; }
        public string  Description { get; set; }
        public byte[]  FileData { get; set; }
        public DateTime  Date { get; set; }
        public int EmpolyeeId { get; set; }
        public Employee? Employee { get; set; }

        //public Attachment(string? name, string? description, byte[]? fileData, DateTime? date, int empolyeeId)
        //{
        //    Name = name;
        //    Description = description;
        //    FileData = fileData;
        //    Date = date;
        //    EmpolyeeId = empolyeeId;
        //}
    }
}
