using Afrodit.Core.Repositories;
using Afrodit.Repositories.DTOs;
using Afrodit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afrodit.Repositories.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
    }
}
