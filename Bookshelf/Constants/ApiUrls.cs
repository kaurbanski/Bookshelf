using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshelf.Constants
{
    public class ApiUrls
    {
        public static string SEARCH_BOOK_BY_PHRASE = "https://www.googleapis.com/books/v1/volumes?printType=books&q=intitle:";
        public static string SEARCH_BOOK_BY_ID = "https://www.googleapis.com/books/v1/volumes/";
    }
}