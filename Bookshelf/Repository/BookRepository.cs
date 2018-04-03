using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Bookshelf.Models;
using System.Data.Entity;

namespace Bookshelf.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IApplicationDbContext _context;

        public BookRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Book Add(Book book)
        {
            return _context.Books.Add(book);
        }

        public async Task<Book> FindByGoogleIdAsync(string id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.GoogleId == id);
        }

        public async Task<Book> FindOrAddAsync(Book book)
        {
            Book result = await FindByGoogleIdAsync(book.GoogleId);
            if (result == null)
            {
                result = Add(book);
            }
            return result;
        }
    }
}