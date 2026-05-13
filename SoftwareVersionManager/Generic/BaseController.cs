using Microsoft.AspNetCore.Mvc;
using SoftwareVersionManager.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SoftwareVersionManager.Generic
{
    public abstract class BaseController<TEntity, TService> : ControllerBase
        where TEntity : class
        where TService : IService<TEntity>
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var result = await _service.ListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity data)
        {
            try
            {
                var result = await _service.CreateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] TEntity data)
        {
            try
            {
                var result = await _service.UpdateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
