using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevTimer.Startup))]
namespace DevTimer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
