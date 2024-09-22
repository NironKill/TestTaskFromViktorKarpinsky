using MediatR;

namespace IndependentTree.Application.Requests.Trees.Read.GetAll
{
    public class GetAllTreeRequest : IRequest<List<GetAllTreeResponce>>
    {
    }
}
