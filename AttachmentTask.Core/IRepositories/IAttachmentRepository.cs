using AttachmentTask.Core.Entites;


namespace AttachmentTask.Core.IRepositories
{
    public interface IAttachmentRepository
    {
        Task AddAsync(Attachment attachment);
        Task<Attachment> GetByIdAsync( int id);
        Task UpdateAsync(Attachment attachment);
        Task<List<int>> GetAttachmentIdsByGroupIdAsync(int groupId);
        Task<List<Attachment>> GetAttachmentDetailsByGroupIdAsync(int groupId);
    }
}
