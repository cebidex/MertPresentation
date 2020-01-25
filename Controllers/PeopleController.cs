using MertPresentation.Dtos;
using MertPresentation.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MertPresentation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleservice;
        public PeopleController(IPeopleService peopleservice)
        {
            _peopleservice = peopleservice;
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(IList<PeopleGetDto>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await _peopleservice.Get();

            return Ok(result);
        }
    }
}