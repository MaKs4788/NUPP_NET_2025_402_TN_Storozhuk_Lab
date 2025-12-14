using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using Setup.REST.Models;

[ApiController]
[Route("api/[controller]")]
public class SimpleComputersController : ControllerBase
{
    private readonly ICrudServiceAsyncDB<ComputerModel> _service;

    public SimpleComputersController(ICrudServiceAsyncDB<ComputerModel> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LegacyComputerDto>>> GetAll()
    {
        var pcs = await _service.ReadAllAsyncDB();
        var result = pcs.Select(pc => new LegacyComputerDto
        {
            Id = pc.Id,
            Name = pc.Name,
            RAM = pc.RAM,
            Storage = pc.Storage
        });

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<LegacyComputerDto>> Get(Guid id)
    {
        try
        {
            var pc = await _service.ReadAsyncDB(id);
            return Ok(new LegacyComputerDto
            {
                Id = pc.Id,
                Name = pc.Name,
                RAM = pc.RAM,
                Storage = pc.Storage
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult> Create([FromBody] LegacyComputerDto dto)
    {
        if (dto == null) return BadRequest();

        var model = new ComputerModel
        {
            Name = dto.Name,
            RAM = dto.RAM,
            Storage = dto.Storage
        };

        await _service.CreateAsyncDB(model);
        dto.Id = model.Id;

        return CreatedAtAction(nameof(Get), new { id = model.Id }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult> Update(Guid id, [FromBody] LegacyComputerDto dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var existing = await _service.ReadAsyncDB(id);
            existing.Name = dto.Name;
            existing.RAM = dto.RAM;
            existing.Storage = dto.Storage;

            await _service.UpdateAsyncDB(existing);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var existing = await _service.ReadAsyncDB(id);
            await _service.RemoveAsyncDB(existing);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
