using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Homework_8_9.Startup))]
namespace Homework_8_9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
