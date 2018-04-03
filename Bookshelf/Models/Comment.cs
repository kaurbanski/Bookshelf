using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookshelf.Models
{
    public class Comment
    {
        public Comment()
        {
            AddedDate = DateTime.UtcNow;
        }

        [Required]
        public int Id { get; private set; }
        [Required]
        public int BookId { get; private set; }
        [Required]
        public string UserId { get; private set; }
        [Required]
        public string Text { get; private set; }
        [Required]
        public DateTime AddedDate { get; private set; }
        public virtual Book Book { get; private set; }
        public virtual ApplicationUser User { get; private set; }

        public static Comment Create(int bookId, string userId)
        {
            return new Comment { BookId = bookId, UserId = userId };
        }
    }
}