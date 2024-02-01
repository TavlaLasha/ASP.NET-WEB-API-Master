using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CurrencyApp.Startup))]
namespace CurrencyApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
