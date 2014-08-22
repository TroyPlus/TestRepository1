using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Troy.Web.Startup))]
namespace Troy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
