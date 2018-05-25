using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshelf.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Bookshelf.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IApplicationDbContext _context;

        public CommentRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public Comment Add(Comment comment)
        {
            Comment result = _context.Comments.Add(comment);
            return result;
        }

        public IQueryable<Comment> GetAllAsQueryable()
        {
            return _context.Comments.AsQueryable();
        }

        public async Task<List<Comment>> GetByGoogleBookId(string googleBookId)
        {
            return await _context.Comments.Where(x => x.Book.GoogleId == googleBookId).OrderByDescending(x => x.AddedDate).ToListAsync();
        }
    }
}