using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloombergGUI.Startup))]
namespace BloombergGUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
