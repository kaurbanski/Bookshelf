using AutoMapper;
using Bookshelf.BookApiConnection;
using Bookshelf.Constants;
using Bookshelf.Models;
using Bookshelf.Models.ViewModels;
using Bookshelf.Repository;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookshelf.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : Controller
    {
        private readonly IApiConnector _apiConnector;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BooksController(IApiConnector apiConnector, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _apiConnector = apiConnector;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("add-book-to-read")]
        // GET: Books
        public ActionResult AddBookToRead()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetBooksByPhrase(string phrase, int startIndex, int size = 30)
        {
            string url = ApiUrls.SEARCH_BOOK_BY_PHRASE + phrase + "&maxResults=" + size + "&startIndex=" + startIndex;
            BooksVolume items = null;

            Uri uri = new Uri(url);
            try
            {
                HttpResponseMessage response = await _apiConnector.Get(uri);
                string message = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<BooksVolume>(message);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return PartialView("SearchBookResultPartial", items);
        }

        [Route("book-details/{id}")]
        [Authorize]
        public async Task<ActionResult> BookDetails(string id)
        {
            Uri uri = new Uri(ApiUrls.SEARCH_BOOK_BY_ID + id);
            Volume book = null;
            try
            {
                HttpResponseMessage response = await _apiConnector.Get(uri);
                string content = await response.Content.ReadAsStringAsync();
                book = JsonConvert.DeserializeObject<Volume>(content);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(book);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddBookshelf(AddBookViewModel model)
        {
            Book book = Book.Create(model);

            bool isShelfInDatabase = await _unitOfWork.BookshelfRepository.IsInDatabase(User.Identity.GetUserId(), model.GoogleId);
            if (isShelfInDatabase)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bookshelf exist in database!");
            }

            try
            {
                Book resultBook = await _unitOfWork.BookRepository.FindOrAddAsync(book);
                Shelf shelf = new Shelf(resultBook.Id, User.Identity.GetUserId());
                _unitOfWork.BookshelfRepository.Add(shelf);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return Json("Added sucessfully!");
        }

        [Authorize]
        public async Task<ActionResult> IsBookshelfInDatabase(string googleBookId)
        {
            bool isInDatabase = false;
            try
            {
                isInDatabase = await _unitOfWork.BookshelfRepository.IsInDatabase(User.Identity.GetUserId(), googleBookId);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            IsAddedToBookshelf added = new IsAddedToBookshelf { IsAdded = isInDatabase };

            return Json(added, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [Route("your-books")]
        public ActionResult UserBooks()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetUserBookshelf()
        {
            try
            {
                var shelfs = await _unitOfWork.BookshelfRepository.GetAllByUserId(User.Identity.GetUserId());
                var data = _mapper.Map<List<ShelfViewModel>>(shelfs);
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ChangeTheReadingOfTheBook(int shelfId)
        {
            var result = await _unitOfWork.BookshelfRepository.ChangeTheReadingStatusOfTheBook(shelfId);
            await _unitOfWork.CompleteAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DeleteShelf(int id)
        {
            try
            {
                await _unitOfWork.BookshelfRepository.Delete(id);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return Json("error");
            }
            return Json("success");
        }

       
    }
}