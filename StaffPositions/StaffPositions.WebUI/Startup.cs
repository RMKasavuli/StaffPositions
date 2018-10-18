using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StaffPositions.WebUI.Startup))]
namespace StaffPositions.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
