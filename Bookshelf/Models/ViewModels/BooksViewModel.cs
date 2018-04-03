using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshelf.Models
{
    public class BooksVolume
    {
        public List<Volume> Items { get; set; } = new List<Volume>();

    }
    public class Volume
    {
        public string Kind { get; set; } = "";
        public string Id { get; set; } = "";
        public string SelfLink { get; set; } = "";
        public VolumeInfo VolumeInfo { get; set; } = new VolumeInfo();

    }

    public class VolumeInfo
    {
        public string Title { get; set; } = "";
        public string Subtitle { get; set; } = "";
        public List<string> Authors { get; set; } = new List<string>();
        public string Publisher { get; set; } = "";
        public string PublishedDate { get; set; } = "";
        public string Description { get; set; } = "";
        public int PageCount { get; set; } = 0;
        public List<string> Categories { get; set; } = new List<string>();
        public decimal AverageRating { get; set; } = 0;
        public int RatingsCount { get; set; } = 0;
        public ImageLink ImageLinks { get; set; } = new ImageLink { Thumbnail = "/Content/Images/image-not-found.jpg" };
    }
    public class ImageLink
    {
        public string Thumbnail { get; set; } = "";
    }

    public class IsAddedToBookshelf
    {
        public bool IsAdded { get; set; }
    }

    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            ImageLink = "/Content/Images/image-not-found.jpg";
        }
        public string Title { get; set; }
        public string GoogleId { get; set; }
        public string Authors { get; set; }
        public string ImageLink { get; set; }
    }

    public class BookViewModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string GoogleId { get; private set; }
        public string Authors { get; set; }
        public string ImageLink { get; set; }
    }
}