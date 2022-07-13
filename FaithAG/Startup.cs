using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FaithAG.Startup))]
namespace FaithAG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
