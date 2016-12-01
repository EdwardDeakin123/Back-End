using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Back_End.Startup))]
namespace Back_End
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
