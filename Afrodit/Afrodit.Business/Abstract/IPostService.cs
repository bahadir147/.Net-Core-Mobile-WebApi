using Afrodit.Repositories.DTOs;
using Afrodit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrodit.Core.Helper;

namespace Afrodit.Business.Abstract
{
    public interface IPostService
    {
        Task<int> GetUserPostCount(int userId);
        Task<PagedList<Post>> GetUserPosts(int userId, PagingParams pagingParams);
    }
}
