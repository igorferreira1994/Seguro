using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllContratacoesQuery : IRequest<IEnumerable<Contratacao>>
{
    // Sem parâmetros
}
