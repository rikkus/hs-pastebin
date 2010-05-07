using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using HS.PasteBin.Model;
using HS.PasteBin.Web.Controllers;
using HS.SqlServer;
using HS.SyntaxHighlighting;

namespace HS.PasteBin.Web
{
    public class PasteBinControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer container;
        private readonly ConnectionInfo connectionInfo;
        private readonly Config config;

        public PasteBinControllerFactory(IContainer container, ConnectionInfo connectionInfo, Config config)
        {
            this.container = container;
            this.connectionInfo = connectionInfo;
            this.config = config;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Paste":
                    return new PasteController
                        (
                        container.Resolve<IPasteRepository>(),
                        container.Resolve<IHighlighter>(),
                        config
                        );

                case "Admin":
                    return new AdminController(connectionInfo);

                default:
                    return base.CreateController(requestContext, controllerName);
            }
        }
    }
}
