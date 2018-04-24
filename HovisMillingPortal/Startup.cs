using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HovisMillingPortal.Startup))]
namespace HovisMillingPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
