using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.SuccessStatus)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.SuccessStatus)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto) // Buradaki passwrod userForRegisterDto içerisinden de gelebilirdi
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email); // kontrol ettin mi alabiliyor muyum?
            if (!userExists.SuccessStatus) // register talebim başarısız olduysa
            {
                return BadRequest(userExists.Message);
            }

            var userToRegister = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(userToRegister.Data);
            if (result.SuccessStatus)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);

        }

        // userExists içersinde dto property leri haricinde register metodunun içerisinde hashlenen passwordSalt ve passwordHash de var.
    }
}
