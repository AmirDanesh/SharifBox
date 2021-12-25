using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceApi.Application.Commands;
using SpaceApi.Application.Queries;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpacesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SpaceListDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] AddSpace.Command command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch(NotSupportedException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SpaceListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _mediator.Send(new GetAllSpaces.Query()));
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetSpaceById(Guid id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetFullSpaceById.Query(id)));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("bySvgId/{svgId}")]
        public async Task<IActionResult> GetSpaceAsync(string svgId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetFullSpaceBySvgId.Query(svgId)));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{spId}")]
        [ProducesResponseType(typeof(List<SpaceListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(EditSpace.Command command, Guid spId)
        {
            if (command.SpaceId != spId)
            {
                return BadRequest("ورودی های درخواست را بررسی کنید");
            }

            return Ok(await _mediator.Send(command));
        }

        [HttpGet("spaceTypes")]
        public async Task<IActionResult> SpaceTypesDropDown()
        {
            return Ok(await _mediator.Send(new GetSpaceTypes.Query()));
        }

        [HttpGet("spaceTypesForReserve")]
        public async Task<IActionResult> SpaceTypesForReserveDropDown()
        {
            return Ok(await _mediator.Send(new GetTypesForReserve.Query()));
        }

    }
}