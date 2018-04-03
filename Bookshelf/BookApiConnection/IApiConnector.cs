using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.BookApiConnection
{
    public interface IApiConnector
    {
        Task<HttpResponseMessage> Get(Uri uri);
    }
}
