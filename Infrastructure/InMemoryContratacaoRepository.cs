using Application.Ports;
using Domain.Entities;
using System.Collections.Concurrent;

namespace Infrastructure.Adapters;

public class InMemoryContratacaoRepository : IContratacaoRepository
{
    private readonly ConcurrentDictionary<string, Contratacao> _contratacoes = new();

    public Task SaveAsync(Contratacao contratacao)
    {
        if (contratacao == null)
            throw new ArgumentNullException(nameof(contratacao));

        _contratacoes.AddOrUpdate(contratacao.Id, contratacao, (key, oldValue) => contratacao);
        return Task.CompletedTask;
    }

    public Task<Contratacao?> GetByIdAsync(string id)
    {
        _contratacoes.TryGetValue(id, out var contratacao);
        return Task.FromResult(contratacao);
    }

    public Task<IEnumerable<Contratacao>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Contratacao>>(_contratacoes.Values.ToList());
    }
}
