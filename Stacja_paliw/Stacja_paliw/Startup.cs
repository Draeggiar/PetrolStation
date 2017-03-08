using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stacja_paliw.Startup))]
namespace Stacja_paliw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
