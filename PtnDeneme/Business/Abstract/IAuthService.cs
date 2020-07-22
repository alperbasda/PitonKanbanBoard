using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Result Register(UserForRegisterDto userForRegisterDto, string password);
        Result Login(UserForLoginDto userForLoginDto);
        Result UserExists(string email);
        Result CreateAccessToken(User user);
    }
}
