using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshelf.Models;
using System.Threading.Tasks;

namespace Bookshelf.Repository
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly IApplicationDbContext _context;
        public QuotationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Quotation quotation)
        {
            _context.Quotations.Add(quotation);
        }

        public Quotation Edit(Quotation quotation)
        {
            _context.Entry(quotation).State = System.Data.Entity.EntityState.Modified;
            return quotation;
        }

        public void RemoveById(int id)
        {
            var entity = _context.Quotations.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                _context.Quotations.Remove(entity);
            }
        }
    }
}