using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnLanguagesWebsite.Startup))]
namespace LearnLanguagesWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
