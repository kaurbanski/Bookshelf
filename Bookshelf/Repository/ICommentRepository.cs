using Bookshelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bookshelf.Repository
{
    public interface ICommentRepository
    {
        Comment Add(Comment comment);
        IQueryable<Comment> GetAllAsQueryable();
        Task<List<Comment>> GetByGoogleBookId(string googleBookId);
    }
}