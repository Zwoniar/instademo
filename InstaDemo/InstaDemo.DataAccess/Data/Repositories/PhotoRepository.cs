using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaDemo.Contracts.DataContracts;
using Mapster;
using InstaDemo.Contracts.DataContracts.Repositories;
using InstaDemo.DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaDemo.DataAccess.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PhotoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            TypeAdapterConfig<Photo, PhotoDetailsDto>.NewConfig().Map(dest => dest.UserName, src => src.User.UserName);
        }

        public IQueryable<PhotoDto> GetAll()
        {
            return _dbContext.Photos.ProjectToType<PhotoDto>();
        }

        public PhotoDto GetById(int id)
        {
            return _dbContext.Photos.FirstOrDefault(p => p.Id == id)?.Adapt<PhotoDto>();
        }

        public PhotoThumbDto GetThumbById(int id)
        {
            return _dbContext.Photos.FirstOrDefault(p => p.Id == id)?.Adapt<PhotoThumbDto>();
        }

        public void Add(PhotoDto photo)
        {
            _dbContext.Photos.Add(photo.Adapt<Photo>());
            _dbContext.SaveChanges();
        }

        public void Delete(int id, string userId = null)
        {
            var photo = _dbContext.Photos.FirstOrDefault(i => i.Id == id);
            if (photo == null)
                return;
            if (userId != null && photo.UserId != userId)
                return;
            _dbContext.Photos.Remove(photo);
            _dbContext.SaveChanges();
        }

        public IQueryable<PhotoDto> GetUserPhotos(string userId)
        {
            return GetAll().Where(p => p.UserId == userId);
        }

        public PhotoDetailsDto GetDetailsById(int id)
        {
            return _dbContext.Photos.Include(p => p.User).FirstOrDefault(p => p.Id == id)?.Adapt<PhotoDetailsDto>();
        }

        public byte[] GetImage(int id)
        {
            return _dbContext.Photos.Where(p => p.Id == id).Select(p => p.Content).FirstOrDefault();
        }
    }
}
