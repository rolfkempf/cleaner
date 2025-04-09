using Application.Queries;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DummyController : ControllerBase
    {
        private readonly GetDummiesQueryHandler _queryHandler;

        public DummyController()
        {
            _queryHandler = new GetDummiesQueryHandler();
        }

        [HttpGet]
        public IActionResult GetDummies()
        {
            var query = new GetDummiesQuery();
            var dummies = _queryHandler.Handle(query);
            var dummyDtos = dummies.Select(d => new DummyDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            return Ok(dummyDtos);
        }
    }
}