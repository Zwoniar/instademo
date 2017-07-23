using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaDemo.ViewModels
{
    public class PhotoDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string UserName { get; set; }
    }
}
