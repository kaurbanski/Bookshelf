using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookshelf.Models
{
    public class Shelf
    {
        public Shelf()
        {

        }
        public Shelf(int bookId, string userId)
        {
            AddedDate = DateTime.Now;
            BeenRead = false;
            BookId = bookId;
            UserId = userId;
        }
        [Required]
        public int Id { get; private set; }
        [Required]
        public int BookId { get; private set; }
        [Required]
        public bool BeenRead { get; private set; }
        [Required]
        public DateTime AddedDate { get; private set; }
        [Required]
        public string UserId { get; private set; }

        public virtual ApplicationUser User { get; private set; }
        public virtual Book Book { get; private set; }

        public void SetBeenRead(bool value)
        {
            BeenRead = value;
        }
    }
}