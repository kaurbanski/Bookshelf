using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bookshelf.Models
{
    public class Book
    {
        public Book()
        {
            ImageLink = "/Content/Images/image-not-found.jpg";
        }

        [Required]
        public int Id { get; private set; }
        [Required]
        public string Title { get; private set; }
        [Required]
        [StringLength(30)]
        [Index(IsUnique = true)]
        public string GoogleId { get; private set; }
        public string Authors { get; set; }
        public string ImageLink { get; set; }
        public virtual ICollection<Shelf> Bookshelfs { get; private set; }
        public virtual ICollection<Comment> Comments { get; private set; }

        public static Book Create(AddBookViewModel model)
        {
            return new Book
            {
                Title = model.Title,
                GoogleId = model.GoogleId,
                ImageLink = model.ImageLink,
                Authors = model.Authors
            };
        }
    }
}