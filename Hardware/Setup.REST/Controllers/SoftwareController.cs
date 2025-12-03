using Microsoft.AspNetCore.Mvc;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using Setup.REST.Models;

namespace Setup.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly ICrudServiceAsyncDB<SoftwareModel> _service;

        public SoftwareController(ICrudServiceAsyncDB<SoftwareModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoftwareDto>>> GetAll()
        {
            var items = await _service.ReadAllAsyncDB();
            return Ok(items.Select(s => new SoftwareDto
            {
                Id = s.Id,
                OS = s.OS,
                OSVersion = s.OSVersion,
                Antivirus = s.Antivirus,
                ComputerId = s.ComputerId
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SoftwareDto>> GetById(Guid id)
        {
            var s = await _service.ReadAsyncDB(id);
            if (s == null) return NotFound();

            return Ok(new SoftwareDto
            {
                Id = s.Id,
                OS = s.OS,
                OSVersion = s.OSVersion,
                Antivirus = s.Antivirus,
                ComputerId = s.ComputerId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateSoftwareDto dto)
        {
            var entity = new SoftwareModel
            {
                Id = Guid.NewGuid(),
                OS = dto.OS,
                OSVersion = dto.OSVersion,
                Antivirus = dto.Antivirus,
                ComputerId = dto.ComputerId
            };

            await _service.CreateAsyncDB(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CreateSoftwareDto dto)
        {
            var s = await _service.ReadAsyncDB(id);
            if (s == null) return NotFound();

            s.OS = dto.OS;
            s.OSVersion = dto.OSVersion;
            s.Antivirus = dto.Antivirus;

            await _service.UpdateAsyncDB(s);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var s = await _service.ReadAsyncDB(id);
            if (s == null) return NotFound();

            await _service.RemoveAsyncDB(s);
            return NoContent();
        }
    }
}
