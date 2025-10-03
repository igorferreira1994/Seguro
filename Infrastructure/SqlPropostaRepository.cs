using Application.Ports;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class SqlPropostaRepository : IPropostaRepository
{
    private readonly SeguroDbContext _context;

    public SqlPropostaRepository(SeguroDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Proposta proposta)
    {
        _context.Propostas.Add(proposta);
        await _context.SaveChangesAsync();
    }

    public async Task<Proposta?> GetByIdAsync(string id)
    {
        return await _context.Propostas.FindAsync(id);
    }

    public async Task<IEnumerable<Proposta>> GetAllAsync()
    {
        return await _context.Propostas.ToListAsync();
    }

    public async Task UpdateStatusAsync(string id, StatusProposta novoStatus)
    {
        var proposta = await _context.Propostas.FindAsync(id);
        if (proposta != null)
        {
            proposta.AlterarStatus(novoStatus);
            await _context.SaveChangesAsync();
        }
    }
}
