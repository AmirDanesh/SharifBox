using FileAPI.Application.Commands;
using FileAPI.Application.Helper;
using FileAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// دریافت عکس پروفایل کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("profilePicture/{id}")]
        public async Task<IActionResult> GetUserProfilePicture(Guid id)
        {
            var path = await _mediator.Send(new GetUserProfilePicture.Query(id));

            if (!System.IO.File.Exists(path))
                return NotFound();

            return PhysicalFile(path, FileHelper.GetContentType(path));
        }

        /// <summary>
        /// Team Logo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("teamLogo/{id}")]
        public async Task<IActionResult> GetTeamLogo(Guid id)
        {
            var path = await _mediator.Send(new GetTeamLogo.Query(id));

            if (!System.IO.File.Exists(path))
                return NotFound();

            return PhysicalFile(path, FileHelper.GetContentType(path));
        }

        /// <summary>
        /// Event Banner
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("eventBanner/{id}")]
        public async Task<IActionResult> GetEventBanner(Guid id)
        {
            var path = await _mediator.Send(new GetEventBanner.Query(id));

            if (!System.IO.File.Exists(path))
                return NotFound();

            return PhysicalFile(path, FileHelper.GetContentType(path));
        }
    }
}