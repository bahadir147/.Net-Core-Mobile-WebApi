using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrodit.Business.Abstract;
using Afrodit.Core.Entities;
using Afrodit.Core.Helper;
using Afrodit.Repositories.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Afrodit.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        #region Injection

        private IPostService _postService;
        private IPhotoService _photoService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostController(IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IPostService postService,
            IFollowerService follwerService,
            IPhotoService photoService)
        {
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
            _photoService = photoService;
        }

        #endregion

        [HttpGet("getuserposts/{userId}")]
        public async Task<IActionResult> GetUserPosts(int userId, [FromBody]PagingParams pagingParams)
        {
            try
            {
                //-1 default aktif kullanıcı dataları getirmeye calısır tokendan alır.
                if (userId == -1)
                {
                    var userClaim = _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault();

                    if (userClaim == null)
                        return BadRequest(new Response<UserLoginDTO>
                        {
                            Error = true,
                            ErrorDescription = "User not found.",
                            StatusCode = StatusCodes.Status400BadRequest
                        });
                    userId = Convert.ToInt32(userClaim.Name);
                }

                List<UserPostDTO> userPostsResponse = new List<UserPostDTO>();

                var posts = await _postService.GetUserPosts(userId, pagingParams);
                foreach (var item in posts.List)
                {
                    userPostsResponse.Add(
                        new UserPostDTO
                        {
                            postData = item,
                            photos = await _photoService.GetPostPhotos(item.Id)
                        });
                }

                Response.Headers.Add("X-Pagination", posts.GetHeader().ToJson());


                return Ok(new Response<List<UserPostDTO>>
                {
                    PagingHeader = posts.GetHeader(),
                    Error = false,
                    StatusCode = StatusCodes.Status200OK,
                    ResponseModel = userPostsResponse
                });

            }
            catch (Exception ex)
            {
                return Ok(new Response<UserPostDTO>
                {
                    Error = true,
                    ErrorDescription = ex.Message,
                    ResponseModel = null,
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
        }
    }
}