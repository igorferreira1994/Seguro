using Application.Commands;
using Application.Ports;
using MediatR;

namespace Application.Handlers;

public class AlterarStatusPropostaCommandHandler : IRequestHandler<AlterarStatusPropostaCommand>
{
    private readonly IPropostaRepository _propostaRepository;

    public AlterarStatusPropostaCommandHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository ?? throw new ArgumentNullException(nameof(propostaRepository));
    }

    public async Task Handle(AlterarStatusPropostaCommand request, CancellationToken cancellationToken)
    {
        await _propostaRepository.UpdateStatusAsync(request.Id, request.NovoStatus);
    }
}
