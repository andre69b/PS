using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectoSeminario.Startup))]
namespace ProjectoSeminario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
