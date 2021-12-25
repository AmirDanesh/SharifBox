using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Domain.Models.User.Role> _roleManager;

        public RolesController(RoleManager<Domain.Models.User.Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> AddRole(string name)
        {
            var res = await _roleManager.CreateAsync(new Domain.Models.User.Role(name));

            if (!res.Succeeded)
                return BadRequest(res.Errors.ToList());

            return Ok();
        }
    }
}
