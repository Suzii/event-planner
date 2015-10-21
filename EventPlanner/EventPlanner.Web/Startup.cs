using Microsoft.Owin;
using Owin;
using EventPlanner.DAL.Repository;

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
            EventRepository.CreateMap();
            PlaceRepository.CreateMap();
            TimeSlotRepository.CreateMap();
            VoteForDateRepository.CreateMap();
            VoteForPlaceRepository.CreateMap();
        }
    }
}
