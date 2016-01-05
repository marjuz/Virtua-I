using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Virtualizer.Startup))]
namespace Virtualizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
