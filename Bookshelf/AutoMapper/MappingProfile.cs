using AutoMapper;
using Bookshelf.Models;
using Bookshelf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshelf.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<ShelfViewModel, Shelf>().ReverseMap();
            CreateMap<BookViewModel, Book>().ReverseMap();
            CreateMap<AddCommentViewModel, Comment>().ReverseMap();
            CreateMap<CommentInfoViewModel, Comment>().ReverseMap();
        }
    }
}