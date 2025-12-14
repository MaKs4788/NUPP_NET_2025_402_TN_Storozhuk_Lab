using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure;
using Setup.Infrastructure.Models;
using Setup.Infrastructure.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ComputerService : ICrudServiceAsyncDB<ComputerModel>
{
    private readonly SetupContext _context;

    public ComputerService(SetupContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ComputerModel>> ReadAllAsyncDB()
    {
        return await _context.Computers
            .Include(c => c.CPU)
            .Include(c => c.GPU)
            .Include(c => c.Software)
            .Include(c => c.Peripheries)
            .ToListAsync();
    }


    public async Task<IEnumerable<ComputerModel>> ReadAllAsyncDB(int page, int amount)
    {
        if (page < 1 || amount < 1) return Enumerable.Empty<ComputerModel>();

        var all = await ReadAllAsyncDB();
        return all.Skip((page - 1) * amount).Take(amount);
    }


    public async Task<ComputerModel> ReadAsyncDB(Guid id)
    {
        var pc = await _context.Computers
            .Include(c => c.CPU)
            .Include(c => c.GPU)
            .Include(c => c.Software)
            .Include(c => c.Peripheries)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (pc == null) throw new KeyNotFoundException($"Computer with Id {id} not found.");
        return pc;
    }


    public async Task<bool> CreateAsyncDB(ComputerModel element)
    {
        if (element == null) return false;

        await _context.Computers.AddAsync(element);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsyncDB(ComputerModel element)
    {
        if (element == null) return false;

        _context.Computers.Update(element);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> RemoveAsyncDB(ComputerModel element)
    {
        if (element == null) return false;

        _context.Computers.Remove(element);
        await _context.SaveChangesAsync();
        return true;
    }


    public IEnumerator<ComputerModel> GetEnumerator()
    {
        return ReadAllAsyncDB().Result.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
