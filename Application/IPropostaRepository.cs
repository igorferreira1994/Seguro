using Domain.Entities;

namespace Application.Ports;

public interface IPropostaRepository
{
    Task SaveAsync(Proposta proposta);
    Task<Proposta?> GetByIdAsync(string id);
    Task<IEnumerable<Proposta>> GetAllAsync();
    Task UpdateStatusAsync(string id, StatusProposta novoStatus);
}
