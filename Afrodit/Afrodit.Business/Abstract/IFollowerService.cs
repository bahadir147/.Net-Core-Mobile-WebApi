using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afrodit.Business.Abstract
{
    public interface IFollowerService
    {
        Task<int> GetUserFollwerCount(int userId);

        Task<int> GetUserFollowingCount(int userId);
    }
}
