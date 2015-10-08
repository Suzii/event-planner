using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventPlanner.Web.Startup))]
namespace EventPlanner.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
