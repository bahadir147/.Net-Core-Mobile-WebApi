using Afrodit.Business.Abstract;
using Afrodit.Repositories.DTOs;
using Afrodit.Repositories.Abstract;
using Afrodit.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Afrodit.Core.Helper;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Diagnostics;
using Afrodit.Core.Enums;
using System.Threading.Tasks;
using Afrodit.Core.Entities;
using System.Linq;

namespace Afrodit.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.Get(x => x.Id == userId);
        }

        public async Task<UserHeadersDTO> GetUserHeader(int userId)
        {
            var user = await GetUserById(userId);

            if (user == null)
                return null;

            return new UserHeadersDTO
            {
                Username = user.Username,
                UserImage = user.Picture,
                Fullname = user.FullName,
                Biography = user.Biography
            };
        }

        public async Task<UserLoginDTO> Login(string username, string password)
        {
            var key = Encoding.ASCII.GetBytes(AppSettingsJson.GetString("AppSettings:Secret"));

            password = CyrptoHash.DecryptString(password, Convert.ToBase64String(key));
            //Sql de şifreli tutuluor.
            password = HashData.SHA512(password);

            var user = await _userRepository.Get(x => x.Username == username && x.Password == password && x.Status == Status.Active);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.ProfileType.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenSecret = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenSecret);

            return new UserLoginDTO
            {
                FullName = user.FullName,
                Id = user.Id,
                Password = null,
                Username = user.Username,
                Token = token
            };
        }

        public async Task<UserLoginDTO> Register(UserRegisterDTO registerParam)
        {
            var Secret = AppSettingsJson.GetString("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(Secret);

#if !DEBUG
            string encPassword = CyrptoHash.DecryptString(registerParam.Password, Convert.ToBase64String(key));
#else
            string encPassword = registerParam.Password;
#endif

            if (_userRepository.Get(x => x.Username == registerParam.Username) != null)
                throw new Exception($"Username is already exist! Username: {registerParam.Username}");

            try
            { MailAddress m = new MailAddress(registerParam.Email); }
            catch (FormatException)
            { throw new Exception($"Email format is incorrect! Email: {registerParam.Email}"); }

            var alreadyUser = await _userRepository.GetList(x => x.Email == registerParam.Email);
            if (alreadyUser.Any())
                throw new Exception($"Email is already exist! Email: {registerParam.Email}");

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(encPassword) && hasUpperChar.IsMatch(encPassword) && hasMinimum8Chars.IsMatch(encPassword);

            if (!isValidated)
                throw new Exception("The password must have at least one large character, one number and a length of more than 8 characters.");

            if (string.IsNullOrEmpty(registerParam.Picture))
                registerParam.Picture = "DefaultImg";

            encPassword = HashData.SHA512(encPassword);

            await _userRepository.Add(new User
            {
                Username = registerParam.Username,
                Biography = registerParam.Biography,
                CreateDate = DateTime.Now,
                Email = registerParam.Email,
                FullName = registerParam.FullName,
                LastSeen = DateTime.Now,
                Password = encPassword,
                Picture = registerParam.Picture,
                Status = Status.Active
            });

            return await Login(registerParam.Username, registerParam.Password);
        }

        public async Task<bool> UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            var user = await _userRepository.Get(x => x.Id == userUpdateDTO.UserId);
            if (user == null)
                throw new Exception($"User Not Found Name: {userUpdateDTO.FullName}");

            user.FullName = userUpdateDTO.FullName;
            user.Biography = userUpdateDTO.Biography;
            user.Picture = userUpdateDTO.Picture;
            user.Email = userUpdateDTO.Email;
            user.PhoneNumber = userUpdateDTO.PhoneNumber;
            user.Status = userUpdateDTO.Status;
            user.SharedProfile = userUpdateDTO.SharedProfile;

            return await _userRepository.Update(user);
        }

    }
}
