using System.Collections.Generic;
using System.Linq;

namespace InstaDemo.Contracts.DataContracts.Repositories
{
    public interface IPhotoRepository
    {
        IQueryable<PhotoDto> GetAll();
        PhotoDto GetById(int id);
        PhotoThumbDto GetThumbById(int id);
        void Add(PhotoDto photo);
        void Delete(int id, string userId = null);
        IQueryable<PhotoDto> GetUserPhotos(string userId);
        PhotoDetailsDto GetDetailsById(int id);
        byte[] GetImage(int id);
    }
}
