using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocHub.Startup))]
namespace DocHub
{
    public partial class Startup
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //}

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }

    }
}
