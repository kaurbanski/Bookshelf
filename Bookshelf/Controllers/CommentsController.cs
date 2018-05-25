using AutoMapper;
using Bookshelf.Models;
using Bookshelf.Models.ViewModels;
using Bookshelf.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookshelf.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetCommentsForBook(string bookId)
        {
            List<Comment> comments = null;
            List<CommentInfoViewModel> model = new List<CommentInfoViewModel>();
            try
            {
                comments = await _unitOfWork.CommentRepository.GetByGoogleBookId(bookId);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            model = _mapper.Map<List<CommentInfoViewModel>>(comments);
            return PartialView("_Comments", model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add(AddCommentViewModel model)
        {
            Comment comment = null;
            Comment result = null;

            try
            {
                var book = await _unitOfWork.BookRepository.FindByGoogleIdAsync(model.GoogleBookId);
                comment = Comment.Create(book.Id, User.Identity.GetUserId());
                comment = _mapper.Map(model, comment);
                result = _unitOfWork.CommentRepository.Add(comment);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return Json("error");
            }
            CommentInfoViewModel returnModel = _mapper.Map<CommentInfoViewModel>(result);
            return Json(returnModel);
        }
    }
}