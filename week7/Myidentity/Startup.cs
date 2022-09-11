using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Myidentity.Startup))]
namespace Myidentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
