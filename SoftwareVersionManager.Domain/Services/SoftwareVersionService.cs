using SoftwareVersionManager.Domain.Generic;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Domain.Services
{
    public class SoftwareVersionService : BaseService<SoftwareVersion>
    {
        public SoftwareVersionService(IRepository<SoftwareVersion> repository) : base(repository)
        {
        }
    }
}
