namespace Domain.Entities;


public class Proposta
{
    public string Id { get; private set; }
    public string NomeCliente { get; private set; }
    public decimal Valor { get; private set; }
    public StatusProposta Status { get; private set; }

    public Proposta(string id, string nomeCliente, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("ID da proposta é obrigatório.");

        if (string.IsNullOrWhiteSpace(nomeCliente))
            throw new ArgumentException("Nome do cliente é obrigatório.");

        if (valor <= 0)
            throw new ArgumentException("Valor da proposta deve ser maior que zero.");

        Id = id;
        NomeCliente = nomeCliente;
        Valor = valor;
        Status = StatusProposta.EmAnalise;
    }

    public void AlterarStatus(StatusProposta novoStatus)
    {
        Status = novoStatus;
    }

    public bool PodeSerContratada() => Status == StatusProposta.Aprovada;
}

public enum StatusProposta
{
    EmAnalise,
    Aprovada,
    Rejeitada
}