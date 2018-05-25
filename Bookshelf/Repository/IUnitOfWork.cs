using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Repository
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IShelfRepository BookshelfRepository { get; }
        ICommentRepository CommentRepository { get; }
        IQuotationRepository QuotationRepository { get; }
        Task<int> CompleteAsync();
    }
}
