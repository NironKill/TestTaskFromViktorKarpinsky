using MediatR;

namespace IndependentTree.Application.Requests.Nodes.Read.GetAll
{
    public class GetAllNodeRequest : IRequest<List<GetAllNodeResponce>>
    {
    }
}
