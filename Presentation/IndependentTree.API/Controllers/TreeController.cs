using AutoMapper;
using IndependentTree.Application.Requests.Trees.Read.GetAll;
using IndependentTree.Application.Requests.Trees.Read.GetById;
using IndependentTree.Application.Requests.Trees.Write.Create;
using IndependentTree.Application.Requests.Trees.Write.Delete;
using IndependentTree.Application.Requests.Trees.Write.Update;
using Microsoft.AspNetCore.Mvc;

namespace IndependentTree.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TreeController : BaseController
    {
        private readonly IMapper _mapper;

        public TreeController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateTreeDTO dto)
        {
            CreateTreeRequest request = _mapper.Map<CreateTreeRequest>(dto);

            CreateTreeResponce response = await Mediator.Send(request);
            return Created("", response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody]UpdateTreeDTO dto, [FromQuery]Guid id)
        {
            UpdateTreeRequest request = _mapper.Map<UpdateTreeRequest>(dto);
            request.Id = id;

            UpdateTreeResponce response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            DeleteTreeRequest request = new DeleteTreeRequest
            {
                Id = id,
            };

            DeleteTreeResponce response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllTreeRequest request = new GetAllTreeRequest();

            List<GetAllTreeResponce> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            GetByIdTreeRequest request = new GetByIdTreeRequest
            {
                TreeId = id
            };
            GetByIdTreeResponce responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}
