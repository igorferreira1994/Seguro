using MediatR;

namespace Application.Commands;

public class ContratarPropostaCommand : IRequest<string>
{
    public string ContratacaoId { get; }
    public string PropostaId { get; }

    public ContratarPropostaCommand(string contratacaoId, string propostaId)
    {
        ContratacaoId = contratacaoId;
        PropostaId = propostaId;
    }
}
