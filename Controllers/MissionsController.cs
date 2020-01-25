using MertPresentation.Dtos;
using MertPresentation.Helpers;
using MertPresentation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MertPresentation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missonservice;
        public MissionsController(IMissionService missonservice)
        {
            _missonservice = missonservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]MissionAddDto model)
        {
            var result = await _missonservice.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(IList<MissionGetDto>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await _missonservice.Get();

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]MissionUpdateDto model)
        {
            var result = await _missonservice.Update(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([BindRequired]Guid id)
        {
            var result = await _missonservice.Delete(id);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
