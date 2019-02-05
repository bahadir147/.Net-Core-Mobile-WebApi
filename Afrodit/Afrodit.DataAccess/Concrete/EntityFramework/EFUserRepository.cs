using Afrodit.Core.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore.SqlServer;
using Afrodit.Repositories.DTOs;
using Afrodit.Repositories.Abstract;
using Afrodit.Entities.Concrete;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Afrodit.Repositories.Concrete.EntityFramework
{
    public class EFUserRepository : EfEntityRepositoryBase<User, AfroditContext>, IUserRepository
    {

    }
}
