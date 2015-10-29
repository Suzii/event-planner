using Microsoft.Owin;
using Owin;
using EventPlanner.DAL.AutoMappers;

[assembly: OwinStartupAttribute(typeof(EventPlanner.Web.Startup))]
namespace EventPlanner.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Set up Web API  -- was in 4th lecture
            //app.UseWebApi(WebApiConfig.Register());

            // Map models
            AutoMappers.Configure();
        }
    }
}
