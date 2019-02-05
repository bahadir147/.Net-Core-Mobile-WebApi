using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrodit.Business.Abstract;
using Afrodit.Repositories.DTOs;
using Afrodit.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Afrodit.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region Injection

        private IUserService _userService;
        private IPostService _postService;
        private IFollowerService _follwerService;
        private IPhotoService _photoService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IPostService postService,
            IFollowerService follwerService,
            IPhotoService photoService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _postService = postService;
            _follwerService = follwerService;
            _photoService = photoService;
        }

        #endregion

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO userParam)
        {
            var user = await _userService.Login(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new Response<UserLoginDTO>
                {
                    Error = true,
                    ErrorDescription = "Username or password is incorrect",
                    StatusCode = StatusCodes.Status400BadRequest
                });

            return Ok(
                new Response<UserLoginDTO>
                {
                    Error = false,
                    ErrorDescription = null,
                    ResponseModel = user,
                    StatusCode = StatusCodes.Status200OK
                });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerParam)
        {
            try
            {
                var user = await _userService.Register(registerParam);
                if (user == null)
                    throw new Exception("Register Error");

                return Ok(new Response<UserLoginDTO>
                {
                    Error = false,
                    ErrorDescription = null,
                    ResponseModel = user,
                    StatusCode = StatusCodes.Status200OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserLoginDTO>
                {
                    Error = true,
                    ErrorDescription = ex.Message,
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
        }

        [HttpGet("getuserheader")]
        public async Task<IActionResult> GetUserHeader()
        {
            var userClaim = _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault();

            if (userClaim == null)
                return BadRequest(new Response<UserLoginDTO>
                {
                    Error = true,
                    ErrorDescription = "User not found.",
                    StatusCode = StatusCodes.Status400BadRequest
                });

            UserHeadersDTO userHeader = new UserHeadersDTO();

            try
            {
                int userid = Convert.ToInt32(userClaim.Name);
                int userPostCount = await _postService.GetUserPostCount(userid);
                userHeader = await _userService.GetUserHeader(userid);

                userHeader.PostCount = userPostCount;

                int userFollwerCount = await _follwerService.GetUserFollwerCount(userid);
                int userFollowingCount = await _follwerService.GetUserFollowingCount(userid);

                userHeader.FollowerCount = userFollwerCount;
                userHeader.FollowingCount = userFollowingCount;

            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserLoginDTO>
                {
                    Error = true,
                    ErrorDescription = $"Internal server error. Error: {ex.Message}",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            return Ok(new Response<UserHeadersDTO>
            {
                Error = false,
                ErrorDescription = null,
                ResponseModel = userHeader,
                StatusCode = StatusCodes.Status200OK
            });

        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                bool ok = await _userService.UpdateUser(userUpdateDTO);

                return Ok(new Response<bool>
                {
                    Error = false,
                    ErrorDescription = null,
                    ResponseModel = ok,
                    StatusCode = StatusCodes.Status200OK
                });

            }
            catch (Exception ex)
            {
                return Ok(new Response<bool>
                {
                    Error = true,
                    ErrorDescription = ex.Message,
                    ResponseModel = false,
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
        }
    }
}