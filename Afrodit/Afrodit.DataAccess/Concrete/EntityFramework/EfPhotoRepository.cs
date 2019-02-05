using Afrodit.Core.Repositories.EntityFramework;
using Afrodit.Entities.Concrete;
using Afrodit.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.Concrete.EntityFramework
{
    public class EFPhotoRepository : EfEntityRepositoryBase<Photo, AfroditContext>, IPhotoRepository
    {
    }
}
