using Application.Ports;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Handlers;

public class GetAllContratacoesQueryHandler : IRequestHandler<GetAllContratacoesQuery, IEnumerable<Contratacao>>
{
    private readonly IContratacaoRepository _contratacaoRepository;

    public GetAllContratacoesQueryHandler(IContratacaoRepository contratacaoRepository)
    {
        _contratacaoRepository = contratacaoRepository ?? throw new ArgumentNullException(nameof(contratacaoRepository));
    }

    public async Task<IEnumerable<Contratacao>> Handle(GetAllContratacoesQuery request, CancellationToken cancellationToken)
    {
        return await _contratacaoRepository.GetAllAsync();
    }
}
