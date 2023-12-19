using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using Microsoft.EntityFrameworkCore;


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

        public async Task<AttachmentsGroup> GetByIdAsync(int id)
        {
            return await _dbContext.AttachmentsGroup
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
