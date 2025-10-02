using Application.Ports;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Handlers;

public class GetAllPropostasQueryHandler : IRequestHandler<GetAllPropostasQuery, IEnumerable<Proposta>>
{
    private readonly IPropostaRepository _propostaRepository;

    public GetAllPropostasQueryHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository ?? throw new ArgumentNullException(nameof(propostaRepository));
    }

    public async Task<IEnumerable<Proposta>> Handle(GetAllPropostasQuery request, CancellationToken cancellationToken)
    {
        return await _propostaRepository.GetAllAsync();
    }
}
