using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LoadFromFile(Server.MapPath(@"~/ClientConfiguration.xml"));
            GrainClient.Initialize(config);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
