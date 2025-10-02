using MediatR;

namespace Application.Commands;

public class CriarPropostaCommand : IRequest<string>
{
    public string Id { get; }
    public string NomeCliente { get; }
    public decimal Valor { get; }

    public CriarPropostaCommand(string id, string nomeCliente, decimal valor)
    {
        Id = id;
        NomeCliente = nomeCliente;
        Valor = valor;
    }


}
