using Application.Commands;
using Application.Ports;
using Domain.Entities;
using MediatR;

namespace Application.Handlers;

public class CreatePropostaCommandHandler : IRequestHandler<CriarPropostaCommand, string>
{
    private readonly IPropostaRepository _propostaRepository;

    public CreatePropostaCommandHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository ?? throw new ArgumentNullException(nameof(propostaRepository));
    }

    public async Task<string> Handle(CriarPropostaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var proposta = new Proposta(request.Id, request.NomeCliente, request.Valor);
            await _propostaRepository.SaveAsync(proposta);
            return proposta.Id;
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException($"Erro ao criar proposta: {ex.Message}", ex);
        }
    }
}
