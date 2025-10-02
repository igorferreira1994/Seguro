using System.ComponentModel.DataAnnotations;

public class PropostaInputDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string NomeCliente { get; set; }

    [Range(1, double.MaxValue)]
    public decimal Valor { get; set; }
}

public class PropostaDisplayDto
{
    public string Id { get; set; }
    public string NomeCliente { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; }
}

public class StatusUpdateDto
{
    [Required]
    public string NovoStatus { get; set; }
}

public class ContratacaoInputDto
{
    [Required]
    public string ContratacaoId { get; set; }

    [Required]
    public string PropostaId { get; set; }
}
