using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookshelf.Models;

namespace Bookshelf.Repository
{
    public interface IShelfRepository
    {
        Shelf Add(Shelf bookshelf);
        Task<bool> IsInDatabase(string userId, string googleBookId);
        Task<IEnumerable<Shelf>> GetAllByUserId(string userId);
        IQueryable<Shelf> GetAllAsQueryable();
        Task<bool> ChangeTheReadingStatusOfTheBook(int shelfId);
        Task Delete(int id);
    }
}
