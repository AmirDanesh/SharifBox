using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Application.Commands;
using UserApi.Application.Queries;
using UserApi.DTOs;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// پروفایل کاربر
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("{phoneNumber}")]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfile(string phoneNumber)
        {
            return Ok(await _mediator.Send(new GetUserProfileByUserName.Query(phoneNumber)));
        }

        /// <summary>
        /// ویرایش اطلاعات کاربری
        /// </summary>
        /// <param name="command"></param>
        /// <param name="identityUserId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{identityUserId}")]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUserProfile([FromBody] EditUserProfile.Command command, Guid identityUserId)
        {
            if (command.IdentityUserId == identityUserId)
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

        /// <summary>
        /// لیست انتخاب کاربران
        /// </summary>
        /// <returns></returns>
        [HttpGet("dropDown")]
        [ProducesResponseType(typeof(List<UsersDropDownDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsersForDropDown()
        {
            var dropDown = await _mediator.Send(new GetUsersDropDown.Query());

            return Ok(dropDown);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsers.Query()));
        }

        /// <summary>
        /// Don't Use == it's for refit
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersName(List<Guid> ids)
        {
            var list = await _mediator.Send(new GetUsersName.Query(ids));

            return Ok(list);
        }

        /// <summary>
        /// Don't Use == it's for refit
        /// </summary>
        /// <param name="identityUserId"></param>
        /// <returns></returns>
        [HttpGet("byId/{id}")]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfileById(Guid id)
        {
            return Ok(await _mediator.Send(new GetUserProfile.Query(id)));
        }
    }
}