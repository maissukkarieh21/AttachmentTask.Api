using AttachmentTask.Core.Entites;


namespace AttachmentTask.Core.IRepositories
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAttachmentsByEmployeeIdAsync(int employeeId);
        Task AddAsync(Attachment attachment);
        Task<Attachment> GetByIdAsync(int id);
        Task UpdateAsync(Attachment attachment);
        Task<IEnumerable<Attachment>> GetAttachmentsByTemporaryEmployeeIdAsync(string temporaryEmployeeId);
    }
}
