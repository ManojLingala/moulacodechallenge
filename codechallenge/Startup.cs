using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(codechallenge.Startup))]
namespace codechallenge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
