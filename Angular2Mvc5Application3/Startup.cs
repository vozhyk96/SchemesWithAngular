using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Schemes.Startup))]
namespace Schemes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
