using Application.Features.Dummy.Queries;
using Application.Features.Dummy.Commands;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DummyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DummyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetDummies()
        {
            var query = new GetDummiesQuery();
            var dummies = _mediator.Send<GetDummiesQuery, List<DummyDto>>(query);
            return Ok(dummies);
        }

        [HttpPost]
        public IActionResult CreateDummy([FromBody] CreateDummyRequest request)
        {
            // For now, just return the received request as a confirmation
            return CreatedAtAction(nameof(GetDummies), new { id = Guid.NewGuid() }, request);
        }
    }
}