using MediatR;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Threading.Tasks;
using TeamApi.Application.Commands;
using TeamApi.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TeamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // TODO : Auth Admin
    public class TeamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// ایجاد تیم جدید
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] AddTeam.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// دریافت تیم با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            return Ok(await _mediator.Send(new GetTeamById.Query(id)));
        }

        /// <summary>
        /// دریافت لیست تیم ها
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(await _mediator.Send(new GetAllTeams.Query()));
        }

        /// <summary>
        /// ویرایش تیم
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTeam(EditTeam.Command command, Guid id)
        {
            if (command.Id == id)
            {
                try
                {
                return Ok(await _mediator.Send(command));
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound(e.Message);
                }
            }
            else
            {
                return BadRequest("ورودی های درخواست را بررسی کنید");
            }
        }
    }
}