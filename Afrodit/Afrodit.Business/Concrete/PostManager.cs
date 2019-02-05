using Afrodit.Business.Abstract;
using Afrodit.Repositories.DTOs;
using Afrodit.Repositories.Abstract;
using Afrodit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afrodit.Core.Enums;
using Afrodit.Core.Helper;

namespace Afrodit.Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostManager(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<int> GetUserPostCount(int userId)
        {
            var userPosts = await _postRepository.GetList(x => x.UserId == userId && x.Status == Status.Active);
            return userPosts.Count();
        }

        public async Task<PagedList<Post>> GetUserPosts(int userId, PagingParams pagingParams)
        {
            var userPosts = await _postRepository.GetList(x => x.UserId == userId && x.Status == Status.Active);
            var quary = userPosts.AsQueryable();
            return new PagedList<Post>(
                quary, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
