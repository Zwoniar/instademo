using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FreeImageAPI;

namespace InstaDemo.Services.Services
{
    public class ThumbnailService
    {
        public const int ThumbSize = 300;
        public static MemoryStream Resize(byte[] fullImage, int? size = null)
        {
            if (!size.HasValue)
                size = ThumbSize;
            using (var stream = new MemoryStream(fullImage))
            using (var original = FreeImageBitmap.FromStream(stream))
            {
                int width, height;
                if (original.Width > original.Height)
                {
                    width = size.Value;
                    height = original.Height * size.Value / original.Width;
                }
                else
                {
                    width = original.Width * size.Value / original.Height;
                    height = size.Value;
                }
                var resized = new FreeImageBitmap(original, width, height);
                var streamSave = new MemoryStream();
                resized.Save(streamSave, FREE_IMAGE_FORMAT.FIF_JPEG,
                     FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB |
                     FREE_IMAGE_SAVE_FLAGS.JPEG_BASELINE);
                streamSave.Seek(0, SeekOrigin.Begin);
                return streamSave;

            }
        }
    }
}
