using Application.Commands;
using Application.Ports;
using Application.Queries;
using DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public static class PropostasController
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("/", () => new
        {
            Name = "SeguroAPI",
            Version = "1.0.0",
            Description = "API para gerenciamento de propostas de seguro",
            Endpoints = new[]
            {
                "POST /propostas - Criar proposta",
                "GET /propostas - Listar propostas",
                "PATCH /propostas/{id}/status - Alterar status",
                "POST /contratacoes - Contratar proposta"
            }
        });

        app.MapPost("/propostas", async (PropostaInputDto input, IMediator mediator) =>
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(input);

            if (!Validator.TryValidateObject(input, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(v => v.ErrorMessage).ToArray();
                return Results.BadRequest(new { Errors = errors });
            }

            var command = new CriarPropostaCommand(input.Id, input.NomeCliente, input.Valor);
            var propostaId = await mediator.Send(command);

            return Results.Created($"/propostas/{propostaId}", new { Id = propostaId });
        });

        app.MapGet("/propostas", async (IMediator mediator) =>
        {
            var query = new GetAllPropostasQuery();
            var propostas = await mediator.Send(query);

            var dtos = propostas.Select(p => new PropostaDisplayDto
            {
                Id = p.Id,
                NomeCliente = p.NomeCliente,
                Valor = p.Valor,
                Status = p.Status.ToString()
            });

            return Results.Ok(dtos);
        });

        app.MapPatch("/propostas/{id}/status", async (string id, StatusUpdateDto input, IMediator mediator) =>
        {
            if (!Enum.TryParse<StatusProposta>(input.NovoStatus, true, out var status))
            {
                return Results.BadRequest(new { Error = "Status inválido." });
            }

            var command = new AlterarStatusPropostaCommand(id, status);
            await mediator.Send(command);

            return Results.Ok(new { Message = "Status atualizado com sucesso." });
        });

        app.MapPost("/contratacoes", async (ContratacaoInputDto input, IMediator mediator) =>
        {
            var command = new ContratarPropostaCommand(input.ContratacaoId, input.PropostaId);
            try
            {
                var id = await mediator.Send(command);
                return Results.Created($"/contratacoes/{id}", new { Id = id });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { Error = ex.Message });
            }
        });
    }
}
