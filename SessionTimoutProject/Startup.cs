using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SessionTimoutProject.Startup))]
namespace SessionTimoutProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
