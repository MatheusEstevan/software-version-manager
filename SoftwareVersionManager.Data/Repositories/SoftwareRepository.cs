using SoftwareVersionManager.Data.Context;
using SoftwareVersionManager.Data.Generic;
using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Data.Repositories
{
    public class SoftwareRepository : BaseRepository<Software>
    {
        public SoftwareRepository(SoftwareVersionManagerDbContext context) : base(context)
        {
        }
    }
}
