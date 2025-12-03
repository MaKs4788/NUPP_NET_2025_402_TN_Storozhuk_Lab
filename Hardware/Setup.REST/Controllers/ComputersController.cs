using Microsoft.AspNetCore.Mvc;
using Setup.REST.Models;
using Setup.Infrastructure.Services;
using Setup.Infrastructure.Models;

namespace Setup.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputersController : ControllerBase
    {
        private readonly ICrudServiceAsyncDB<ComputerModel> _service;

        public ComputersController(ICrudServiceAsyncDB<ComputerModel> service)
        {
            _service = service;
        }

        // GET api/computers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> GetAll()
        {
            var items = await _service.ReadAllAsyncDB();

            return Ok(items.Select(m => new ComputerDto
            {
                Id = m.Id,
                Name = m.Name,
                RAM = m.RAM,
                Storage = m.Storage
            }));
        }

        // GET api/computers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerDto>> Get(Guid id)
        {
            var comp = await _service.ReadAsyncDB(id);
            if (comp == null) return NotFound();

            return Ok(new ComputerDto
            {
                Id = comp.Id,
                Name = comp.Name,
                RAM = comp.RAM,
                Storage = comp.Storage
            });
        }

        // POST api/computers
        [HttpPost]
        public async Task<ActionResult> Create(CreateComputerDto dto)
        {
            var entity = new ComputerModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                RAM = dto.RAM,
                Storage = dto.Storage
            };

            await _service.CreateAsyncDB(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, null);
        }

        // PUT api/computers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CreateComputerDto dto)
        {
            var comp = await _service.ReadAsyncDB(id);
            if (comp == null) return NotFound();

            comp.Name = dto.Name;
            comp.RAM = dto.RAM;
            comp.Storage = dto.Storage;

            await _service.UpdateAsyncDB(comp);

            return NoContent();
        }

        // DELETE api/computers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var comp = await _service.ReadAsyncDB(id);
            if (comp == null) return NotFound();

            await _service.RemoveAsyncDB(comp);

            return NoContent();
        }
    }
}
