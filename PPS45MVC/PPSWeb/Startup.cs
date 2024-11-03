using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PPSWeb.Startup))]
namespace PPSWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
