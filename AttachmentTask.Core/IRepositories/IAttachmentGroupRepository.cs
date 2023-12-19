using AttachmentTask.Core.Entites;

namespace AttachmentTask.Core.IRepositories
{
    public interface IAttachmentGroupRepository
    {
        Task AddAsync(AttachmentsGroup attachmentsGroup);
        Task UpdateAsync(AttachmentsGroup attachmentsGroup);
        Task<AttachmentsGroup> GetByIdAsync(int id);
    }
}
