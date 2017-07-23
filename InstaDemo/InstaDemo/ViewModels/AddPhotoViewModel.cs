using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InstaDemo.ViewModels
{
    public class AddPhotoViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Photo")]
        public IFormFile Content { get; set; }
    }
}
