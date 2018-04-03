using Bookshelf.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Bookshelf.BookApiConnection
{
    public class ApiConnector : IApiConnector
    {
        public HttpClient Client { get; private set; }

        public async Task<HttpResponseMessage> Get(Uri uri)
        {
            ConnectionClient connectionClient = new ConnectionClient();
            Client = connectionClient.Create();
            HttpResponseMessage response = await Client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiConnectionException();
            }
            return response;
        }
    }
}