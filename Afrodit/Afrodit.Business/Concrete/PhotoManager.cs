using Afrodit.Business.Abstract;
using Afrodit.Entities.Concrete;
using Afrodit.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afrodit.Business.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        public PhotoManager(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<List<Photo>> GetPostPhotos(int postId)
        {
           return await _photoRepository.GetList(x => x.PostId == postId);
        }
    }
}
