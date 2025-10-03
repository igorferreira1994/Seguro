using Application.Ports;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class SqlContratacaoRepository : IContratacaoRepository
{
    private readonly SeguroDbContext _context;

    public SqlContratacaoRepository(SeguroDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Contratacao contratacao)
    {
        _context.Contratacoes.Add(contratacao);
        await _context.SaveChangesAsync();
    }

    public async Task<Contratacao?> GetByIdAsync(string id)
    {
        return await _context.Contratacoes.FindAsync(id);
    }

    public async Task<IEnumerable<Contratacao>> GetAllAsync()
    {
        return await _context.Contratacoes.ToListAsync();
    }
}
