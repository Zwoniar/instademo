using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Google.Cloud.Vision.V1;

namespace InstaDemo.Services.Services
{
    public class GVisionService
    {
        private string _url;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Url which wil be used to send image to GVision API, eg. http://host.com/Image/{id} </param>
        public GVisionService(string url)
        {
            _url = url;
        }
        public IReadOnlyList<EntityAnnotation> Recognize(int id)
        {
            var client = ImageAnnotatorClient.Create();

            var response = client.DetectLabels(Image.FetchFromUri(_url + id));
            return response;
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    Console.WriteLine(annotation.Description);
            }
        }
    }
}
