using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OurFirstWebApplication.Startup))]
namespace OurFirstWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
