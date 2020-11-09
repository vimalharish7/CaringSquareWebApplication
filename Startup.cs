using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaringSquareApp.Startup))]
namespace CaringSquareApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
