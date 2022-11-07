using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IOptionsSnapshot<JwtSettings> _optionsSnapshot;
        private readonly IRoleService _roleService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;


        //todo: add error handling, validate input params,  logging, authorization header 
        public AccountController(IMapper mapper, IAccountService accountService,
            IOptionsSnapshot<JwtSettings> optionsSnapshot, IRoleService roleService, 
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _accountService = accountService;
            _optionsSnapshot = optionsSnapshot;
            _roleService = roleService;
            _logger = logger;
            _configuration = configuration;
        }
        
        // GET api/Account/get_user
        [HttpGet("get_user")]
        public async Task<IActionResult> GetUserInfo([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Not valid id!");
            }
            object? user = null;
            try
            {
                user = await _accountService.GetUserInfoAsync(userId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Not valid id!");
            }
            
            return Ok(user);
        }


        // POST api/Account/sign_up
        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            if (model.Password != model.PasswordConfirm || !model.Email.Contains('@'))
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = null;

            try
            {
                var userInfo = _mapper.Map<SignUpDto>(model);

                var roles = _configuration.GetSection("UserRoles")[ "CreateAccount"];

               result = await _accountService.SignUpAsync(userInfo, roles);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ModelState);
            }
            
            return Created(Url.Link(nameof(GetUserInfo), null), null);
        }

        // POST api/Account/sing_in
        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var userCreds = _mapper.Map<SignInDto>(model);

            var user =  await _accountService.SignInAsync(userCreds);

            if (user is null) return BadRequest();

            var roles = await _roleService.GetRolesAsync(user);
            var accessToken = JwtHelper.GenerateJwt(user, roles, _optionsSnapshot.Value);
            return Ok(accessToken) ;
        }
        
        // DELETE api/Account/delete_account
        [HttpDelete("hard_delete_account/{id}")]
        public async Task<IActionResult> HardDeleteAccount(string id)
        {
            await _accountService.HardDeleteAccountAsync(id);
            return Ok();
        }
        
        // DELETE api/Account/delete_account
        [HttpDelete("delete_account/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            await _accountService.SoftDeleteAccountAsync(id);
            return Ok();
        }
    }
}