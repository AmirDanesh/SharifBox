using IdentityApi.Application.Commands;
using IdentityApi.Application.Queries;
using IdentityApi.Application.Refit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///ورود
        /// </summary>
        /// <param name="request">
        /// PasswordCheckQuery
        /// </param>
        /// <returns>jwt token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LogInasync(PasswordCheckQuery request)
        {
            var res = await _mediator.Send(request);
            var user = await _mediator.Send(new GetUserByUserNameQuery(request.UserName));
            var userRoles = await _mediator.Send(new GetUserRoles.Query(request.UserName));

            if(user == null)
            {
                return NotFound("کاربری یافت نشد");
            }

            if (res.Succeeded)
            {
                var rest = RestService.For<IUserAPI>(Startup.Configuration["refitLinks:userService"]);
                var usersName = await rest.GetUser(new Guid(user.Id.ToString()));
                var jwt = await _mediator.Send(new GenerateJwtTokenQuery(user, userRoles));
                var cookieOption = new CookieOptions
                {
                    HttpOnly = true,
                    Path = this.Request.PathBase.HasValue ? this.Request.PathBase.ToString() : "/",
                    Secure = this.Request.IsHttps
                };
                Response.Cookies.Append("aud", JsonConvert.SerializeObject(new Guid()), cookieOption);
                Response.Cookies.Append("aut", jwt, cookieOption);
                return Ok(new { token = jwt, fullName = usersName.FirstName + " " + usersName.LastName, id = user.Id });
            }
            else
            {
                if (res.IsLockedOut)
                    return Unauthorized("حساب کاربری شما به علت چند بار وارد کردن اشتباه رمز قفل شده است");

                if (res.IsNotAllowed)
                    return Unauthorized("حساب کاربری شما قفل می‌باشد");

                return Unauthorized("نام کاربری یا رمز عبور اشتباه است");
            }
        }

        /// <summary>
        ///مرحله اول ثبت کاربر
        /// </summary>
        /// <param name="request">
        /// RegisterUserCommand
        /// </param>
        /// <returns>UserID</returns>
        [Produces("text/plain")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand request)
        {
            try
            {
                return Ok((await _mediator.Send(request)).ToString());
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///مرحله دوم ثبت کاربر
        /// </summary>
        /// <param name="request">
        /// ValidateVerificationCommand
        /// </param>
        /// <returns>jwt token</returns>
        [HttpPost("phonenumberverify")]
        public async Task<IActionResult> PhoneVerificationAsync([FromBody] ValidateVerificationCommand request)
        {
            try
            {
                var jwt = await _mediator.Send(request);
                return Ok(new { token = jwt });
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch(NotSupportedException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///مرحله سوم ثبت کاربر
        /// </summary>
        /// <param name="request">
        /// SetPassWordCommand
        /// </param>
        /// <returns>jwt token</returns>
        [Authorize]
        [HttpPost("setpassword")]
        public async Task<IActionResult> CompleteRegisterAsync([FromBody] SetPassWordCommand request)
        {
            return Ok((await _mediator.Send(request)).ToString());
        }

        /// <summary>
        /// تغییر گذرواژه
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassWord(ChangePassword.Command command)
        {

            var res = await _mediator.Send(new ChangePassword.Command(User.GetUserName(), command.CurrentPassword, command.NewPassword));
            if (res.Succeeded)
            {
                return Ok(res.Succeeded);
            }
            return BadRequest(res.Errors.FirstOrDefault().Description);
        }

        /// <summary>
        /// فراموشی رمز عبور
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassWord(SetValidationCodeCommand command)
        {
            var user = await _mediator.Send(new GetUserByUserNameQuery(command.PhoneNumber));

            if(user == null)
            {
                return NotFound("کاربری یافت نشد");
            }

            return Ok(await _mediator.Send(command));
        }
    }
}