using Microsoft.AspNetCore.Mvc;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using Setup.REST.Models;

namespace Setup.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CPUController : ControllerBase
    {
        private readonly ICrudServiceAsyncDB<CPUModel> _service;

        public CPUController(ICrudServiceAsyncDB<CPUModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUDto>>> GetAll()
        {
            var items = await _service.ReadAllAsyncDB();
            return Ok(items.Select(cpu => new CPUDto
            {
                Id = cpu.Id,
                Brand = cpu.Brand,
                Model = cpu.Model,
                Cores = cpu.Cores,
                Threads = cpu.Threads,
                Frequency = cpu.Frequency,
                ComputerId = cpu.ComputerId
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CPUDto>> Get(Guid id)
        {
            var cpu = await _service.ReadAsyncDB(id);
            if (cpu == null) return NotFound();

            return Ok(new CPUDto
            {
                Id = cpu.Id,
                Brand = cpu.Brand,
                Model = cpu.Model,
                Cores = cpu.Cores,
                Threads = cpu.Threads,
                Frequency = cpu.Frequency,
                ComputerId = cpu.ComputerId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCPUDto dto)
        {
            var entity = new CPUModel
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Model = dto.Model,
                Cores = dto.Cores,
                Threads = dto.Threads,
                Frequency = dto.Frequency,
                ComputerId = dto.ComputerId
            };

            await _service.CreateAsyncDB(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CreateCPUDto dto)
        {
            var cpu = await _service.ReadAsyncDB(id);
            if (cpu == null) return NotFound();

            cpu.Brand = dto.Brand;
            cpu.Model = dto.Model;
            cpu.Cores = dto.Cores;
            cpu.Threads = dto.Threads;
            cpu.Frequency = dto.Frequency;

            await _service.UpdateAsyncDB(cpu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var cpu = await _service.ReadAsyncDB(id);
            if (cpu == null) return NotFound();

            await _service.RemoveAsyncDB(cpu);
            return NoContent();
        }
    }

}
