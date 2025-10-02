namespace Domain.Entities;

public class Contratacao
{
    public string Id { get; private set; }
    public string PropostaId { get; private set; }
    public DateTime DataContratacao { get; private set; }

    public Contratacao(string id, string propostaId)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("ID da contratação é obrigatório.");

        if (string.IsNullOrWhiteSpace(propostaId))
            throw new ArgumentException("ID da proposta é obrigatório.");

        Id = id;
        PropostaId = propostaId;
        DataContratacao = DateTime.UtcNow;
    }
}