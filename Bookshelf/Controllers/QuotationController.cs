using Bookshelf.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshelf.Controllers
{
    public class QuotationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuotationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("your-quotations")]
        public ActionResult Index()
        {
            return View();
        }
    }
}