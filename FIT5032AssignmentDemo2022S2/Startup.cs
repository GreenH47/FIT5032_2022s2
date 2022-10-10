using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIT5032AssignmentDemo2022S2.Startup))]
namespace FIT5032AssignmentDemo2022S2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
