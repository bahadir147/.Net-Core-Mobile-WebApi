using Afrodit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afrodit.Business.Abstract
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPostPhotos(int postId);
    }
}
