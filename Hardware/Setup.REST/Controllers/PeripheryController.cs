using Microsoft.AspNetCore.Mvc;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using Setup.REST.Models;

namespace Setup.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeripheryController : ControllerBase
    {
        private readonly ICrudServiceAsyncDB<PeripheryModel> _service;

        public PeripheryController(ICrudServiceAsyncDB<PeripheryModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeripheryDto>>> GetAll()
        {
            var items = await _service.ReadAllAsyncDB();
            return Ok(items.Select(p => new PeripheryDto
            {
                Id = p.Id,
                DeviceType = p.DeviceType,
                Brand = p.Brand,
                ConnectionType = p.ConnectionType,
                ComputerId = p.ComputerId
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeripheryDto>> GetById(Guid id)
        {
            var p = await _service.ReadAsyncDB(id);
            if (p == null) return NotFound();

            return Ok(new PeripheryDto
            {
                Id = p.Id,
                DeviceType = p.DeviceType,
                Brand = p.Brand,
                ConnectionType = p.ConnectionType,
                ComputerId = p.ComputerId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePeripheryDto dto)
        {
            var entity = new PeripheryModel
            {
                Id = Guid.NewGuid(),
                DeviceType = dto.DeviceType,
                Brand = dto.Brand,
                ConnectionType = dto.ConnectionType,
                ComputerId = dto.ComputerId
            };

            await _service.CreateAsyncDB(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CreatePeripheryDto dto)
        {
            var p = await _service.ReadAsyncDB(id);
            if (p == null) return NotFound();

            p.DeviceType = dto.DeviceType;
            p.Brand = dto.Brand;
            p.ConnectionType = dto.ConnectionType;

            await _service.UpdateAsyncDB(p);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var p = await _service.ReadAsyncDB(id);
            if (p == null) return NotFound();

            await _service.RemoveAsyncDB(p);
            return NoContent();
        }
    }
}
