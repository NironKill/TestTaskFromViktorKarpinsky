using AutoMapper;
using IndependentTree.Application.Requests.Nodes.Read.GetAll;
using IndependentTree.Application.Requests.Nodes.Read.GetById;
using IndependentTree.Application.Requests.Nodes.Write.Create;
using IndependentTree.Application.Requests.Nodes.Write.Delete;
using IndependentTree.Application.Requests.Nodes.Write.Update;
using Microsoft.AspNetCore.Mvc;

namespace IndependentTree.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NodeController : BaseController
    {
        private readonly IMapper _mapper;

        public NodeController(IMapper mapper) => _mapper = mapper;
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody]CreateNodeDTO dto)
        {
            CreateNodeRequest request = _mapper.Map<CreateNodeRequest>(dto);

            CreateNodeResponse response = await Mediator.Send(request);
            return Created("", response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody]UpdateNodeDTO dto, [FromQuery] Guid nodeId, [FromQuery] Guid treeId)
        {
            UpdateNodeRequest request = _mapper.Map<UpdateNodeRequest>(dto);
            request.NodeId = nodeId;
            request.TreeId = treeId;

            UpdateNodeResponce response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromQuery]Guid nodeId, [FromQuery]Guid treeId)
        {
            DeleteNodeRequest request = new DeleteNodeRequest
            {
                NodeId = nodeId,
                TreeId = treeId
            };
            DeleteNodeResponse response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllNodeRequest request = new GetAllNodeRequest();

            List<GetAllNodeResponce> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromQuery]Guid nodeId, [FromQuery]Guid treeId)
        {
            GetByIdNodeRequest request = new GetByIdNodeRequest
            {
                NodeId = nodeId,
                TreeId = treeId
            };
            GetByIdNodeResponce responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}
