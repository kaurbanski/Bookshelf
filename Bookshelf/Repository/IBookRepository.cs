using Bookshelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Repository
{
    public interface IBookRepository
    {
        Task<Book> FindOrAddAsync(Book book);
        Task<Book> FindByGoogleIdAsync(string id);
        Book Add(Book book);
    }
}
