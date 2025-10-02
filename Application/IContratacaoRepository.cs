using Domain.Entities;

namespace Application.Ports;

public interface IContratacaoRepository
{
    Task SaveAsync(Contratacao contratacao);
    Task<Contratacao?> GetByIdAsync(string id);
    Task<IEnumerable<Contratacao>> GetAllAsync();
}
