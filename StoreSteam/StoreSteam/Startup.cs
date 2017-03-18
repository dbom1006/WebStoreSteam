using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreSteam.Startup))]
namespace StoreSteam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
