using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Application.Commands;
using UserApi.Application.Queries;
using UserApi.DTOs;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// ایجاد مهارت جدید
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SkillDropDownDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNewSkill(AddSkill.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        ///  لیست مهارت ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<SkillDropDownDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSkills()
        {
            return Ok(await _mediator.Send(new GetAllSkills.Query()));
        }
    }
}