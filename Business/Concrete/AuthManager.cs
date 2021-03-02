using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService; //normalde manager larda kendi dal interface i enjekte edilir, farklı bir dal dan destek almak için service hizmeti(IXService) tanımlanmalıdır.
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }


        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto) // buradaki password userForregisterDto içerisinden de gelebilirdi.
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);
            return new SuccessDataResult<User>(user, AspectMessages.UserRegistered);

        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(AspectMessages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(AspectMessages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, AspectMessages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.GetByEmail(email); // eğer true döndüyse, ilgili mail adresi boşta, kullanılabilir
            if (result.Data == null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(AspectMessages.UserAlreadyExists);

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, AspectMessages.AccessTokenCreated);

        }
    }

}