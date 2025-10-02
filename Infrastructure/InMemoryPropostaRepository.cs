using Application.Ports;
using Domain.Entities;
using System.Collections.Concurrent;

namespace Infrastructure.Adapters;

public class InMemoryPropostaRepository : IPropostaRepository
{
    private readonly ConcurrentDictionary<string, Proposta> _propostas = new();

    public Task SaveAsync(Proposta proposta)
    {
        if (proposta == null)
            throw new ArgumentNullException(nameof(proposta));

        _propostas.AddOrUpdate(proposta.Id, proposta, (key, oldValue) => proposta);
        return Task.CompletedTask;
    }

    public Task<Proposta?> GetByIdAsync(string id)
    {
        _propostas.TryGetValue(id, out var proposta);
        return Task.FromResult(proposta);
    }

    public Task<IEnumerable<Proposta>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Proposta>>(_propostas.Values.ToList());
    }

    public Task UpdateStatusAsync(string id, StatusProposta novoStatus)
    {
        if (_propostas.TryGetValue(id, out var proposta))
        {
            proposta.AlterarStatus(novoStatus);
        }

        return Task.CompletedTask;
    }
}
