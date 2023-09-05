using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HairGallery.Startup))]
namespace HairGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
