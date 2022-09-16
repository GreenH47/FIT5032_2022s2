using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clinic.Startup))]
namespace Clinic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
