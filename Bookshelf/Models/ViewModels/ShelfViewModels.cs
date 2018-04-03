using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshelf.Models
{
    public class ShelfViewModel
    {
        public int Id { get; private set; }

        public int BookId { get; private set; }

        public bool BeenRead { get; private set; }

        public DateTime AddedDate { get; private set; }
        public BookViewModel Book { get; private set; }
    }
}