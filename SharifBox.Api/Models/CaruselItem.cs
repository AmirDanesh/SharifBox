using System.Collections.Generic;

namespace SharifBox.Api.Models
{
    public class CaruselItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public List<CaruselItem> GetCaruselItems()
        {
            return new List<CaruselItem>
            {
                new CaruselItem
                {
                    Title = "ایتم اول",
                   Description="توضیحات",
                   PictureUrl ="/image/download.jpg",
                },
                new CaruselItem
                {
                    Title = "ایتم دوم",
                   Description="توضیحات",
                   PictureUrl ="/image/download (1).jpg",
                },
                new CaruselItem
                {
                    Title = "ایتم سوم",
                   Description="توضیحات",
                   PictureUrl ="/image/download (2).jpg",
                },
                new CaruselItem
                {
                    Title = "ایتم چهارم",
                   Description="توضیحات",
                   PictureUrl ="/image/download (3).jpg",
                },
            };
        }
    }
}