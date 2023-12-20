using AttachmentTask.Core.Entites;
using AttachmentTask.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AttachmentTask.Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _dbContext;
        public AttachmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        //public async Task<IEnumerable<Attachment>> GetAttachmentsByEmployeeIdAsync(int employeeId)
        //{
        //    return await _dbContext.Attachments.Where(a => a.EmpolyeeId == employeeId).ToListAsync();
        //}

        public async Task AddAsync(Attachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException(nameof(attachment));
            }

            _dbContext.Attachments.Add(attachment);
            _dbContext.SaveChanges();
        }


        public async Task UpdateAsync(Attachment attachment)
        {
            _dbContext.Attachments.Update(attachment);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Attachment> GetByIdAsync(int id)
        {
            return _dbContext.Attachments.FirstOrDefault(a => a.Id == id);
        }

        public async Task<List<int>> GetAttachmentIdsByGroupIdAsync(int groupId)
        {
            var attachmentIds = await _dbContext.Employees
                .Where(e => e.AttachmentsGroupId == groupId)
                .SelectMany(e => e.AttachmentsGroup.Attachments.Select(a => a.Id))
                .ToListAsync();

            return attachmentIds;
        }

        public async Task<List<Attachment>> GetAttachmentDetailsByGroupIdAsync(int groupId)
        {
            var attachments = await _dbContext.Employees
                .Where(e => e.AttachmentsGroupId == groupId)
                .SelectMany(e => e.AttachmentsGroup.Attachments.Select(a => new Attachment
                {
                    Id = a.Id,
                    Name = a.Name,
                    FileData = a.FileData
                }))
                .ToListAsync();

            return attachments;
        }


    }
}
