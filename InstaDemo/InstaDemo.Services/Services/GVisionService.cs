using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Google.Cloud.Vision.V1;
using InstaDemo.Contracts.DataContracts;
using Mapster;
using InstaDemo.Contracts.DataContracts.Services;

namespace InstaDemo.Services.Services
{
    public class GVisionService : IGVisionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Url which wil be used to send image to GVision API, eg. http://host.com/Image/{id} </param>
        /// <returns></returns>
        public IReadOnlyList<GVisionImageResponse> Recognize(string url)
        {
            var client = ImageAnnotatorClient.Create();

            //var response = client.DetectLabels(Image.FetchFromUri(_url + id));
            url = "https://www.topgear.com/sites/default/files/styles/16x9_1280w/public/cars-car/image/2015/02/buyers_guide_-_vw_golf_gti_2014_-_front_quarter.jpg?itok=lwiRtMw0";
            var response = client.DetectLabels(Image.FetchFromUri(url));
            return response.Adapt<IReadOnlyList<GVisionImageResponse>>();
        }
    }
}
