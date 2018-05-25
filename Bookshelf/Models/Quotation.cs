using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookshelf.Models
{
    public class Quotation
    {
        [Required]
        public int Id { get; }
        [Required]
        public String Content { get; private set; }
        public int QuotationId { get; private set; }
        [Required]
        public int UserId { get; private set; }

        public virtual Quotation QuotationEntity { get; private set; }
        public virtual ApplicationUser User { get; private set; }
    }
}