using AttachmentTask.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentTask.Core.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(int id);

        Task<IEnumerable<Employee>> ListAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);

    }
}
