using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaDemo.Contracts.Common;
using InstaDemo.Contracts.DataContracts;
using InstaDemo.Contracts.DataContracts.Repositories;
using InstaDemo.Contracts.DataContracts.Services;

namespace InstaDemo.Services.Services
{
    public class PhotoService : IPhotoService
    {
        public IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public IQueryable<PhotoDto> GetAll()
        {
            return _photoRepository.GetAll();
        }

        public CommonResult<PhotoDto> GetById(int id)
        {
            var photo = _photoRepository.GetById(id);
            if (photo == null)
                return CommonResult<PhotoDto>.Failure(PhotoErrorMessage(id));
            return CommonResult<PhotoDto>.Success(photo);
        }

        private static string PhotoErrorMessage(int id)
        {
            return $"Photo with id: {id} does not exists in database";
        }

        public void Add(PhotoDto photo)
        {
            _photoRepository.Add(photo);
        }


        public IQueryable<PhotoDto> GetUserPhotos(string userId)
        {
            return _photoRepository.GetUserPhotos(userId);
        }

        public void Delete(int id, string userId = null)
        {
            _photoRepository.Delete(id, userId);
        }

        public CommonResult<PhotoDetailsDto> GetDetailsById(int id)
        {
            var photo = _photoRepository.GetDetailsById(id);
            if (photo == null)
                return CommonResult<PhotoDetailsDto>.Failure(PhotoErrorMessage(id));
            return CommonResult<PhotoDetailsDto>.Success(photo);
        }

        public CommonResult<PhotoThumbDto> GetThumbById(int id)
        {
            var photo = _photoRepository.GetThumbById(id);
            if (photo == null)
                return CommonResult<PhotoThumbDto>.Failure(PhotoErrorMessage(id));
            return CommonResult<PhotoThumbDto>.Success(photo);
        }

        public CommonResult<byte[]> GetImage(int id)
        {
            var photo = _photoRepository.GetImage(id);
            if (photo == null)
                return CommonResult<byte[]>.Failure(PhotoErrorMessage(id));
            return CommonResult<byte[]>.Success(photo);
        }
    }
}
