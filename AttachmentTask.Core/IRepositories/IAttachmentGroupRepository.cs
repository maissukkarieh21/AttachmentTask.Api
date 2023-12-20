using AttachmentTask.Core.Entites;

namespace AttachmentTask.Core.IRepositories
{
    public interface IAttachmentGroupRepository
    {
        Task AddAsync(AttachmentsGroup attachmentsGroup);
        Task UpdateAsync(AttachmentsGroup attachmentsGroup);
        Task<List<int>> GetAttachmentIdsByGroupIdAsync(int groupId);
    }
}
