using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllPropostasQuery : IRequest<IEnumerable<Proposta>>
{
    // Sem parâmetros
}
