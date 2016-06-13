using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrowdTouring_Projeto.Startup))]
namespace CrowdTouring_Projeto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
