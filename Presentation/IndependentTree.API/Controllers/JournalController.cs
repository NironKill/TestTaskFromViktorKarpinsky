using AutoMapper;
using IndependentTree.Application.Requests.Magazine.Read.GetAll;
using IndependentTree.Application.Requests.Magazine.Read.GetById;
using IndependentTree.Application.Requests.Magazine.Read.GetByStatusCode;
using Microsoft.AspNetCore.Mvc;

namespace IndependentTree.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class JournalController : BaseController
    {
        private readonly IMapper _mapper;

        public JournalController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllJournalRequest request = new GetAllJournalRequest();

            List<GetAllJournalResponce> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            GetByIdJournalRequest request = new GetByIdJournalRequest
            {
                ExceptionId = id
            };
            GetByIdJournalResponce responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByStatusCode([FromQuery]int statuscode)
        {
            GetByStatusCodeRequest request = new GetByStatusCodeRequest
            {
                StatusCode = statuscode
            };
            List<GetByStatusCodeResponce> responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}
