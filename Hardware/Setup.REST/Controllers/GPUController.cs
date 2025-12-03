using Microsoft.AspNetCore.Mvc;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using Setup.REST.Models;

namespace Setup.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GPUController : ControllerBase
    {
        private readonly ICrudServiceAsyncDB<GPUModel> _service;

        public GPUController(ICrudServiceAsyncDB<GPUModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPUDto>>> GetAll()
        {
            var gpus = await _service.ReadAllAsyncDB();
            return Ok(gpus.Select(g => new GPUDto
            {
                Id = g.Id,
                Brand = g.Brand,
                Model = g.Model,
                VRAM = g.VRAM,
                MemoryType = g.MemoryType,
                CoreClock = g.CoreClock,
                ComputerId = g.ComputerId
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GPUDto>> GetById(Guid id)
        {
            var gpu = await _service.ReadAsyncDB(id);
            if (gpu == null) return NotFound();

            return Ok(new GPUDto
            {
                Id = gpu.Id,
                Brand = gpu.Brand,
                Model = gpu.Model,
                VRAM = gpu.VRAM,
                MemoryType = gpu.MemoryType,
                CoreClock = gpu.CoreClock,
                ComputerId = gpu.ComputerId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGPUDto dto)
        {
            var entity = new GPUModel
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Model = dto.Model,
                VRAM = dto.VRAM,
                MemoryType = dto.MemoryType,
                CoreClock = dto.CoreClock,
                ComputerId = dto.ComputerId
            };

            await _service.CreateAsyncDB(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CreateGPUDto dto)
        {
            var gpu = await _service.ReadAsyncDB(id);
            if (gpu == null) return NotFound();

            gpu.Brand = dto.Brand;
            gpu.Model = dto.Model;
            gpu.VRAM = dto.VRAM;
            gpu.MemoryType = dto.MemoryType;
            gpu.CoreClock = dto.CoreClock;

            await _service.UpdateAsyncDB(gpu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var gpu = await _service.ReadAsyncDB(id);
            if (gpu == null) return NotFound();

            await _service.RemoveAsyncDB(gpu);
            return NoContent();
        }
    }
}
