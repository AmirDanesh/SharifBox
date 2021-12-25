using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpaceApi.Application.Commands;
using SpaceApi.Application.Queries;
using System;
using System.Threading.Tasks;

namespace SpaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //TODO Authorize
    public class ReserveController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public ReserveController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("spacesForReserve")]
        public async Task<IActionResult> GetSpaces(GetSpacesForReserve.Query commad)
        {
            return Ok(await _mediator.Send(commad));
        }

        /// <summary>
        /// پرداخت
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("payment")]
        public async Task<IActionResult> Pay(Pay.Command command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (OperationCanceledException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ZarinCallBack/{paymentId}")]
        public async Task<IActionResult> ZarinCallBack(Guid paymentId)
        {
            var status = HttpContext.Request.Query["Status"];
            var authority = HttpContext.Request.Query["Authority"];
            await _mediator.Send(new FinalizeReserve.Command(paymentId, status, authority));
            return Redirect(_configuration["returnUrl"]);
        }

        [HttpGet("userReservations/{userName}")]
        public async Task<IActionResult> UserReservationList(string userName)
        {
            return Ok(await _mediator.Send(new GetReservationByUserName.Query(userName)));
        }

        [HttpGet("userPayments/{userName}")]
        public async Task<IActionResult> UserPaymentsList(string userName)
        {
            return Ok(await _mediator.Send(new GetPaymentsByUserName.Query(userName)));
        }
    }
}
