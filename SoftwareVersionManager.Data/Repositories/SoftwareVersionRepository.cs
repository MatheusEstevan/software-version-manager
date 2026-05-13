using SoftwareVersionManager.Data.Context;
using SoftwareVersionManager.Data.Generic;
using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Data.Repositories
{
    public class SoftwareVersionRepository : BaseRepository<SoftwareVersion>
    {
        public SoftwareVersionRepository(SoftwareVersionManagerDbContext context) : base(context)
        {
        }
    }
}
