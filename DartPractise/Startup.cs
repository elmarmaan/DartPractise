using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DartPractise.Startup))]
namespace DartPractise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
