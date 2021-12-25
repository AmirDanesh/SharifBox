using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Application.Commands;
using NewsApi.Application.Queries;
using NewsApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// ثبت رویداد جدید
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EventDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNewEvent(AddEvent.Command command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// نمایش رویداد
        /// </summary>
        /// <param name="eId"></param>
        /// <returns></returns>
        [HttpGet("{eId}")]
        [ProducesResponseType(typeof(EventDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEvent(Guid eId)
        {
            return Ok(await _mediator.Send(new GetEventById.Query(eId)));
        }

        /// <summary>
        /// لیست رویداد ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EventDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEvents()
        {
            return Ok(await _mediator.Send(new GetAllEvents.Query()));
        }

        /// <summary>
        /// لیست رویداد ها برای لندینگ پیج
        /// </summary>
        /// <returns></returns>
        [HttpGet("landing")]
        public async Task<IActionResult> GetAllEventsForLanding()
        {
            return Ok(await _mediator.Send(new GetAllEventsForLanding.Query()));
        }

        /// <summary>
        /// آپدیت رویداد
        /// </summary>
        /// <param name="command"></param>
        /// <param name="eId"></param>
        /// <returns></returns>
        [HttpPut("{eId}")]
        public async Task<IActionResult> UpdateEvent([FromBody] EditEvent.Command command, Guid eId)
        {
            if (command.Id == eId)
            {
                try
                {
                    return Ok(await _mediator.Send(command));
                }
                catch (ArgumentException e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("ورودی های درخواست را بررسی کنید");
            }
        }

        //TODO check senario
        [HttpPatch]
        public async Task<IActionResult> ChangeShowOnLanding(ChangeEventShowOnLanding.Command command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}