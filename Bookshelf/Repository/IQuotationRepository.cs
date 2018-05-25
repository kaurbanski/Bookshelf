using Bookshelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshelf.Repository
{
    public interface IQuotationRepository
    {
        void Add(Quotation quotation);
        Quotation Edit(Quotation quotation);
        void RemoveById(int id);
    }
}