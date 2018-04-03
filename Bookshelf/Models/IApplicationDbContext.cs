using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bookshelf.Models
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Shelf> Bookshelfs { get; set; }
        DbSet<Comment> Comments { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}