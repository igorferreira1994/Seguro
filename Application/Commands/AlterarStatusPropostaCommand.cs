using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AlterarStatusPropostaCommand : IRequest
{
    public string Id { get; }
    public StatusProposta NovoStatus { get; }

    public AlterarStatusPropostaCommand(string id, StatusProposta novoStatus)
    {
        Id = id;
        NovoStatus = novoStatus;
    }
}
