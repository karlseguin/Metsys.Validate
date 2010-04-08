using Spark;
using Spark.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;

namespace Metsys.Validate.MvcSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
#if DEBUG
        private const bool _debug = true;
#else
        private const bool _debug = false;
#endif

        protected void Application_Start()
        {
            Validator.InitializeFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        
            var viewFactory = new SparkViewFactory(new SparkSettings()
                .AddNamespace("System")
                .AddNamespace("System.Collections.Generic")
                .AddNamespace("Metsys.Validate.Mvc") 
                .SetAutomaticEncoding(true)
                .SetDebug(_debug));

            ViewEngines.Engines.Add(viewFactory);
            
            RegisterRoutes(RouteTable.Routes);
        }


        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Step1"});
        }
    }
}