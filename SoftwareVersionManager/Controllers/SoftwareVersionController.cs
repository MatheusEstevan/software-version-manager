using Microsoft.AspNetCore.Mvc;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Generic;

namespace SoftwareVersionManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareVersionController : BaseController<SoftwareVersion, IService<SoftwareVersion>>
    {
        public SoftwareVersionController(IService<SoftwareVersion> service) : base(service)
        {
        }
    }
}
