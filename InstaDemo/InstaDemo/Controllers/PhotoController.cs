using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Cloud.Vision.V1;
using InstaDemo.Contracts.DataContracts;
using InstaDemo.Contracts.DataContracts.Services;
using InstaDemo.DataAccess.Data.Models;
using InstaDemo.Services.Services;
using InstaDemo.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;


namespace InstaDemo.Controllers
{
    public class PhotoController : BaseController
    {
        private IPhotoService _photoService;
        public PhotoController(IPhotoService photoService, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _photoService = photoService;
        }

        public async Task<IActionResult> List()
        {
            var photos = await _photoService.GetAll().ProjectToType<PhotoDataThumbViewModel>().ToListAsync();
            return View(photos);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddPhotoViewModel());
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddPhotoViewModel photo)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.Content.CopyToAsync(memoryStream);
                    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(photo.Content.ContentDisposition);
                    var photoContent = memoryStream.ToArray();
                    var user = await GetCurrentUserAsync();
                    var thumb = ThumbnailService.Resize(photoContent).ToArray();
                    var photoDto = CreatePhoto(photo, user.Id, parsedContentDisposition.FileName, photoContent, thumb);
                    _photoService.Add(photoDto);
                }
            }

            //TODO some confirmation for user that Photo added successfully
            return RedirectToAction(nameof(MyList));
        }

        public async Task<IActionResult> MyList()
        {
            var user = await GetCurrentUserAsync();
            var photos = await _photoService.GetUserPhotos(user.Id).ProjectToType<PhotoDataThumbViewModel>().ToListAsync();

            return View(photos);
        }

        [Route("/Image/{id}")]

        public async Task<IActionResult> Image(int id)
        {
            var imageResult = _photoService.GetById(id);
            if (!imageResult.IsSuccess)
                return StatusCode(404);

            var stream = new MemoryStream(imageResult.Item.Content);
            return new FileStreamResult(stream, GetMimeMapping(imageResult.Item.FileName));
        }


        [Route("/Thumb/{id}")]

        public async Task<IActionResult> Thumb(int id)
        {
            var imageResult = _photoService.GetThumbById(id);
            if (!imageResult.IsSuccess)
                return StatusCode(404);

            var resizedImageStream = ThumbnailService.Resize(imageResult.Item.ThumbContent);
            return new FileStreamResult(resizedImageStream, "image/jpeg");
        }

        public ActionResult Details(int id)
        {
            var details = _photoService.GetDetailsById(id);
            if (!details.IsSuccess)
                return StatusCode(404);

            return View(details.Item);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await GetCurrentUserAsync();
            _photoService.Delete(id, user.Id);

            //TODO some confirmation for user that Photo deleted successfully
            return RedirectToAction(nameof(MyList));
        }

        [Authorize]
        public async Task<IActionResult> GVision()
        { 
            return Json("");
        }

        private static PhotoDto CreatePhoto(AddPhotoViewModel photo, string userId,
        string fileName, byte[] photoContent, byte[] thumb)
        {
            var photoDto = new PhotoDto
            {
                UserId = userId,
                CreationDate = DateTime.UtcNow,
                FileName = fileName.Replace("\\", "").Replace("\"", ""),
                Content = photoContent,
                Title = photo.Title,
                ThumbContent = thumb,
                Description = photo.Description
            };
            return photoDto;
        }
    }
}
