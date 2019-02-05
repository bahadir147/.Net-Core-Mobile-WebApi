using Afrodit.Core.Repositories.EntityFramework;
using Afrodit.Repositories.Abstract;
using Afrodit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.Concrete.EntityFramework
{
    public class EFPostRepository : EfEntityRepositoryBase<Post, AfroditContext>, IPostRepository
    {
    }
}
