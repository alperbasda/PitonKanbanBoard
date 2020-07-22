using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebUI.ControllerExtensions;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return View().Error(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken((User)userToLogin.Data);
            if (result.Success)
            {
                var opClaims = _userService.GetClaims((User)userToLogin.Data);
                var claims = new[] {
                    new Claim(ClaimTypes.Name, ((User)userToLogin.Data).FirstName),
                    new Claim(ClaimTypes.PrimarySid, ((User)userToLogin.Data).Id.ToString()),
                    new Claim(ClaimTypes.Role, string.Join(',' ,opClaims.Select(c=>c.Name)))
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }

            return View().Error(userToLogin.Message);
        }

        [HttpPost]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
                return View().Error(userExists.Message);

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (!registerResult.Success)
                return View().Error(registerResult.Message);

            var result = _authService.CreateAccessToken((User)registerResult.Data);
            if (result.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            return View().Error(result.Message);
        }
    }
}