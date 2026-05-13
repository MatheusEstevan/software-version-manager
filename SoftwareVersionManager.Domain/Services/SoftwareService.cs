using SoftwareVersionManager.Domain.Generic;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Domain.Services
{
    public class SoftwareService : BaseService<Software>
    {
        public SoftwareService(IRepository<Software> repository) : base(repository)
        {
        }
    }
}
