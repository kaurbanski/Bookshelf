using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshelf.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace Bookshelf.Repository
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly IApplicationDbContext _context;

        public ShelfRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Shelf Add(Shelf bookshelf)
        {
            return _context.Bookshelfs.Add(bookshelf);
        }

        public async Task<bool> IsInDatabase(string userId, string googleBookId)
        {
            bool isInDatabase = false;
            var result1 = await _context.Bookshelfs.FirstOrDefaultAsync(x => x.UserId == userId);
            var result2 = await _context.Bookshelfs.FirstOrDefaultAsync(x => x.Book.GoogleId == googleBookId);
            Shelf result = await _context.Bookshelfs.Where(x => x.UserId == userId && x.Book.GoogleId == googleBookId).FirstOrDefaultAsync();

            if (result != null)
            {
                isInDatabase = true;
            }
            return isInDatabase;
        }

        public IQueryable<Shelf> GetAllAsQueryable()
        {
            var result = _context.Bookshelfs.AsQueryable();
            return result;
        }

        public async Task<IEnumerable<Shelf>> GetAllByUserId(string userId)
        {
            var shelfs = await GetAllAsQueryable().Where(x => x.UserId == userId).ToListAsync();
            return shelfs;
        }

        public async Task<bool> ChangeTheReadingStatusOfTheBook(int shelfId)
        {
            Shelf shelf = await _context.Bookshelfs.FirstOrDefaultAsync(x => x.Id == shelfId);
            if (shelf.BeenRead)
            {
                shelf.SetBeenRead(false);
            }
            else
            {
                shelf.SetBeenRead(true);
            }
            return shelf.BeenRead;
        }

        public async Task Delete(int id)
        {
            var result = await _context.Bookshelfs.FirstOrDefaultAsync(x => x.Id == id);
            _context.Bookshelfs.Remove(result);
        }
    }
}