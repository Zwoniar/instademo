using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaDemo.Contracts.Common;

namespace InstaDemo.Contracts.DataContracts.Services
{
    public interface IPhotoService
    {
        IQueryable<PhotoDto> GetAll();
        IQueryable<PhotoDto> GetUserPhotos(string userId);
        CommonResult<PhotoDto> GetById(int id);
        CommonResult<PhotoThumbDto> GetThumbById(int id);
        CommonResult<byte[]> GetImage(int id);
        CommonResult<PhotoDetailsDto> GetDetailsById(int id);
        void Add(PhotoDto photo);
        void Delete(int id, string userId = null);
    }
}
