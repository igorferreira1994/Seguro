using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class SeguroDbContext : DbContext
{
    public SeguroDbContext(DbContextOptions<SeguroDbContext> options) : base(options) { }

    public DbSet<Proposta> Propostas => Set<Proposta>();
    public DbSet<Contratacao> Contratacoes => Set<Contratacao>();
}
