using System;


namespace InstaDemo.Contracts.DataContracts
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Content { get; set; }
        public byte[] ThumbContent { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
    }

    public class PhotoThumbDto
    {
        public byte[] ThumbContent { get; set; }
        public string FileName { get; set; }
    }

    public class PhotoDetailsDto : PhotoDto
    {
        public string UserName { get; set; }
    }
}
