using AutoMapper;
using Contracts;
using Entities.Dto.User;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Server.ActionFilters;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }
        /// <summary>
        /// Get available queries 
        /// </summary>
        /// <param></param>
        /// <returns>All avaiable queries in header</returns>
        /// <response code="200">Returns all avaiable queries in header</response>
        [HttpOptions]
        [ProducesResponseType(200)]
        public IActionResult GetProductsOptions()
        {
            Response.Headers.Add("Allow", "OPTIONS, POST");
            return Ok();
        }

        /// <summary>
        /// Registration new user
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        /// <response code="201">Registration was success</response>
        /// <response code="400">Model for registration is null or user is already exists</response>
        /// <response code="422">Model for registration is invalid</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user, "Client");

            return StatusCode(201);
        }

        /// <summary>
        /// Authentication with login and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>JWT</returns>
        /// <response code="200">Authentication was success</response>
        /// <response code="400">Model for authorization is null</response>
        /// <response code="401">Wrong login or password</response>
        /// <response code="422">Model for authorization is invalid</response>

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return Unauthorized();
            }
            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}
