using Application.Commands;
using Application.Ports;
using Domain.Entities;
using MediatR;

namespace Application.Handlers;

public class ContratarPropostaCommandHandler : IRequestHandler<ContratarPropostaCommand, string>
{
    private readonly IPropostaRepository _propostaRepository;
    private readonly IContratacaoRepository _contratacaoRepository;

    public ContratarPropostaCommandHandler(
        IPropostaRepository propostaRepository,
        IContratacaoRepository contratacaoRepository)
    {
        _propostaRepository = propostaRepository ?? throw new ArgumentNullException(nameof(propostaRepository));
        _contratacaoRepository = contratacaoRepository ?? throw new ArgumentNullException(nameof(contratacaoRepository));
    }

    public async Task<string> Handle(ContratarPropostaCommand request, CancellationToken cancellationToken)
    {
        var proposta = await _propostaRepository.GetByIdAsync(request.PropostaId);

        if (proposta == null)
            throw new InvalidOperationException("Proposta não encontrada.");

        if (!proposta.PodeSerContratada())
            throw new InvalidOperationException("Proposta não está aprovada para contratação.");

        var contratacao = new Contratacao(request.ContratacaoId, request.PropostaId);
        await _contratacaoRepository.SaveAsync(contratacao);

        return contratacao.Id;
    }
}
