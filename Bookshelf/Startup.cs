using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookshelf.Startup))]
namespace Bookshelf
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
