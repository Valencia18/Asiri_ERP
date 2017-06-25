using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asiri_ERP.Startup))]
namespace Asiri_ERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
