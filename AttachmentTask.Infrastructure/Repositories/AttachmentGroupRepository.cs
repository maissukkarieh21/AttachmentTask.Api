using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace AttachmentTask.Infrastructure.Repositories
{
    public class AttachmentGroupRepository: IAttachmentGroupRepository
    {
        private readonly AppDbContext _dbContext;
        public AttachmentGroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(AttachmentsGroup attachmentsGroup)
        {
            if (attachmentsGroup == null)
            {
                throw new ArgumentNullException(nameof(attachmentsGroup));
            }

            _dbContext.AttachmentsGroup.Add(attachmentsGroup);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateAsync(AttachmentsGroup attachmentsGroup)
        {
            _dbContext.AttachmentsGroup.Update(attachmentsGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<int>> GetAttachmentIdsByGroupIdAsync(int groupId)
        {
            var attachmentIds = await _dbContext.AttachmentsGroup
                .Where(group => group.Id == groupId)
                .SelectMany(group => group.Attachments.Select(attachment => attachment.Id))
                .ToListAsync();

            return attachmentIds;
        }



    }
}
