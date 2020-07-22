using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public Result Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new Result
            {
                Message = "Kullanıcı başarıyla kaydedildi",
                Success = true
            };
        }

        public Result Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new Result
                {
                    Success = false,
                    Message = Messages.UserNotFound
                };
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new Result
                {
                    Success = false,
                    Message = Messages.PasswordError
                };
            }

            return new Result
            {
                Success = true,
                Message = "Giriş Başarılı",
                Data = userToCheck
            };
        }

        public Result UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new Result
                {
                    Success = false,
                    Message = Messages.UserAlreadyExists
                };
            }
            return new Result
            {
                Success = true
            };
        }

        public Result CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new Result
            {
                Message = Messages.AccessTokenCreated,
                Success = true,
                Data = accessToken
            };
        }
    }
}
