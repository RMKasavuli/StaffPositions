using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapSite3.Startup))]
namespace BootstrapSite3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
