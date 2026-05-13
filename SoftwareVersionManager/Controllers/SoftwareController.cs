using Microsoft.AspNetCore.Mvc;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Generic;

namespace SoftwareVersionManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : BaseController<Software, IService<Software>>
    {
        public SoftwareController(IService<Software> service) : base(service)
        {
        }
    }
}
