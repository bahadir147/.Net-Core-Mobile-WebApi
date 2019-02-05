using Afrodit.Business.Abstract;
using Afrodit.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afrodit.Business.Concrete
{
    public class FollowerManager : IFollowerService
    {
        private readonly IFollowerRepository _followerRepository;
        public FollowerManager(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<int> GetUserFollowingCount(int userId)
        {
            var userFollowings = await _followerRepository.GetList(x => x.FollowerId == userId);
            var followings = userFollowings.Select(m => new { m.FollowerId, m.UserId }).Distinct();
            return followings.Count();
        }

        public async Task<int> GetUserFollwerCount(int userId)
        {
            var userFollwers = await _followerRepository.GetList(x => x.UserId == userId);
            var followers = userFollwers.Select(m => new { m.FollowerId, m.UserId }).Distinct();
            return followers.Count();
        }
    }
}
