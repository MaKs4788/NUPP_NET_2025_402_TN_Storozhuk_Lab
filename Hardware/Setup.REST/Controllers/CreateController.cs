using Microsoft.AspNetCore.Mvc;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using Setup.REST.Models;

[ApiController]
[Route("api/[controller]")]
public class ComputersController : ControllerBase
{
    private readonly ICrudServiceAsyncDB<ComputerModel> _service;

    public ComputersController(ICrudServiceAsyncDB<ComputerModel> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComputerDto>>> GetAll()
    {
        var pcs = await _service.ReadAllAsyncDB();

        var result = pcs.Select(pc => MapToDto(pc));
        return Ok(result);
    }
  
    [HttpGet("{id}")]
    public async Task<ActionResult<ComputerDto>> Get(Guid id)
    {
        try
        {
            var pc = await _service.ReadAsyncDB(id);
            return Ok(MapToDto(pc));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateComputerDto dto)
    {
        if (dto == null) return BadRequest();

        var model = MapToModel(dto);
        await _service.CreateAsyncDB(model);

        return CreatedAtAction(nameof(Get), new { id = model.Id }, MapToDto(model));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] CreateComputerDto dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var existing = await _service.ReadAsyncDB(id);

            existing.Name = dto.Name;
            existing.RAM = dto.RAM;
            existing.Storage = dto.Storage;

            if (dto.CPU != null)
            {
                existing.CPU ??= new CPUModel();
                existing.CPU.Brand = dto.CPU.Brand;
                existing.CPU.Model = dto.CPU.Model;
                existing.CPU.Cores = dto.CPU.Cores;
                existing.CPU.Threads = dto.CPU.Threads;
                existing.CPU.Frequency = dto.CPU.Frequency;
                existing.CPU.Type = dto.CPU.Type;
            }

            if (dto.GPU != null)
            {
                existing.GPU ??= new GPUModel();
                existing.GPU.Brand = dto.GPU.Brand;
                existing.GPU.Model = dto.GPU.Model;
                existing.GPU.VRAM = dto.GPU.VRAM;
                existing.GPU.MemoryType = dto.GPU.MemoryType;
                existing.GPU.CoreClock = dto.GPU.CoreClock;
                existing.GPU.Type = dto.GPU.Type;
            }

            if (dto.Software != null)
            {
                existing.Software ??= new SoftwareModel();
                existing.Software.OS = dto.Software.OS;
                existing.Software.OSVersion = dto.Software.OSVersion;
                existing.Software.Antivirus = dto.Software.Antivirus;
            }

            existing.Peripheries = dto.Peripheries
                .Select(p => new PeripheryModel
                {
                    DeviceType = p.DeviceType,
                    Brand = p.Brand,
                    ConnectionType = p.ConnectionType
                }).ToList();

            await _service.UpdateAsyncDB(existing);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
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

    private static ComputerDto MapToDto(ComputerModel pc) => new()
    {
        Id = pc.Id,
        Name = pc.Name,
        RAM = pc.RAM,
        Storage = pc.Storage,
        CPU = pc.CPU != null ? new CPUDto
        {
            Brand = pc.CPU.Brand,
            Model = pc.CPU.Model,
            Cores = pc.CPU.Cores,
            Threads = pc.CPU.Threads,
            Frequency = pc.CPU.Frequency,
            Type = pc.CPU.Type
        } : null,
        GPU = pc.GPU != null ? new GPUDto
        {
            Brand = pc.GPU.Brand,
            Model = pc.GPU.Model,
            VRAM = pc.GPU.VRAM,
            MemoryType = pc.GPU.MemoryType,
            CoreClock = pc.GPU.CoreClock,
            Type = pc.GPU.Type
        } : null,
        Software = pc.Software != null ? new SoftwareDto
        {
            OS = pc.Software.OS,
            OSVersion = pc.Software.OSVersion,
            Antivirus = pc.Software.Antivirus
        } : null,
        Peripheries = pc.Peripheries?.Select(p => new PeripheryDto
        {
            DeviceType = p.DeviceType,
            Brand = p.Brand,
            ConnectionType = p.ConnectionType
        }).ToList() ?? new List<PeripheryDto>()
    };

    private static ComputerModel MapToModel(CreateComputerDto dto) => new()
    {
        Name = dto.Name,
        RAM = dto.RAM,
        Storage = dto.Storage,
        CPU = dto.CPU != null ? new CPUModel
        {
            Brand = dto.CPU.Brand,
            Model = dto.CPU.Model,
            Cores = dto.CPU.Cores,
            Threads = dto.CPU.Threads,
            Frequency = dto.CPU.Frequency,
            Type = dto.CPU.Type
        } : null,
        GPU = dto.GPU != null ? new GPUModel
        {
            Brand = dto.GPU.Brand,
            Model = dto.GPU.Model,
            VRAM = dto.GPU.VRAM,
            MemoryType = dto.GPU.MemoryType,
            CoreClock = dto.GPU.CoreClock,
            Type = dto.GPU.Type
        } : null,
        Software = dto.Software != null ? new SoftwareModel
        {
            OS = dto.Software.OS,
            OSVersion = dto.Software.OSVersion,
            Antivirus = dto.Software.Antivirus
        } : null,
        Peripheries = dto.Peripheries.Select(p => new PeripheryModel
        {
            DeviceType = p.DeviceType,
            Brand = p.Brand,
            ConnectionType = p.ConnectionType
        }).ToList()
    };
}
