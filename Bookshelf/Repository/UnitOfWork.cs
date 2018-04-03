using Bookshelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bookshelf.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public IBookRepository BookRepository { get; private set; }
        public IShelfRepository BookshelfRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            BookRepository = new BookRepository(_context);
            BookshelfRepository = new ShelfRepository(_context);
            CommentRepository = new CommentRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}