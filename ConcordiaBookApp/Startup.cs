using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConcordiaBookApp.Startup))]
namespace ConcordiaBookApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
