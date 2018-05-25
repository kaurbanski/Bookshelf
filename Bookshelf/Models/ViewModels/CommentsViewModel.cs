using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookshelf.Models.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public string GoogleBookId { get; set; }
        [Required]
        public string Text { get; set; }
    }

    public class CommentInfoViewModel
    {
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public string Text { get; private set; }
        public DateTime AddedDate { get; private set; }
        public UserCommentViewModel User { get; private set; }
    }
}