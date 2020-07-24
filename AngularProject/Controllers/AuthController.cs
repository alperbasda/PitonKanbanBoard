using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (!userToLogin.Success)
            {
                return Ok(userToLogin);
            }

            var user = (User)userToLogin.Data;
            var result = _authService.CreateAccessToken(user);
            if (result.Success)
            {
                result.Message = user.Id.ToString();
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpGet("logout")]
        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpPost("register")]
        public ActionResult Register(UserForLoginDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            UserForRegisterDto register = new UserForRegisterDto();
            register.Email = userForRegisterDto.Email;
            register.Password = userForRegisterDto.Password;
            register.FirstName = "Api Kullanıcı Ad";
            register.LastName = "Api Kullanıcı Soyad";

            var registerResult = _authService.Register(register, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(((User)registerResult.Data));
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}