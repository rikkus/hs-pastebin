using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Builder;
using HS.PasteBin.Data;
using HS.PasteBin.Model;
using HS.SqlServer;
using HS.SyntaxHighlighting;

namespace HS.PasteBin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string WebConfigHighlightExePathKey = "HighlightExePath";
        private const string WebConfigDefaultLanguagesKey = "DefaultLanguages";
        private const string WebConfigSqlServerNameKey = "SqlServerName";
        private const string WebConfigSqlServerInstanceNameKey = "SqlServerInstanceName";
        private const string WebConfigSqlServerUserNameKey = "SqlServerUserName";
        private const string WebConfigSqlServerPasswordKey = "SqlServerPassword";
        private const string WebConfigDatabaseNameKey = "DatabaseName";
        private const string WebConfigMaxPastesInRecentViewKey = "MaxPastesInRecentView";
        private const string WebConfigMaxPreviewCharactersKey = "MaxPreviewCharacters";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Install", "Install", new { controller = "Admin", action = "Install" });
            routes.MapRoute("Default", "", new { controller = "Paste", action = "Create" });
            routes.MapRoute("Create", "Create", new { controller = "Paste", action = "Create" });
            routes.MapRoute("Recent", "Recent", new { controller = "Paste", action = "Recent" });
            routes.MapRoute("Save", "Save", new { controller = "Paste", action = "Save" });
            routes.MapRoute("Show", "{id}", new { controller = "Paste", action = "Show", id = "" });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory
                (new PasteBinControllerFactory(CreateContainer(), WebConfigConnectionInfo, Config));
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register
                (creator => new Highlighter(WebConfigurationManager.AppSettings[WebConfigHighlightExePathKey]))
                .As<IHighlighter>();

            builder.Register
                (creator => new SqlPasteRepository(WebConfigConnectionInfo, new RealDateStamper()))
                .As<IPasteRepository>();

            return builder.Build();
        }

        private static ConnectionInfo WebConfigConnectionInfo
        {
            get
            {
                return WebConfigurationManager.AppSettings["SqlServerWindowsAuth"] == "True"
                           ? new ConnectionInfo
                                 (
                                 WebConfigurationManager.AppSettings[WebConfigSqlServerNameKey],
                                 WebConfigurationManager.AppSettings[WebConfigSqlServerInstanceNameKey],
                                 WebConfigurationManager.AppSettings[WebConfigDatabaseNameKey]
                                 )
                           : new ConnectionInfo
                                 (
                                 WebConfigurationManager.AppSettings[WebConfigSqlServerNameKey],
                                 WebConfigurationManager.AppSettings[WebConfigSqlServerInstanceNameKey],
                                 WebConfigurationManager.AppSettings[WebConfigSqlServerUserNameKey],
                                 WebConfigurationManager.AppSettings[WebConfigSqlServerPasswordKey],
                                 WebConfigurationManager.AppSettings[WebConfigDatabaseNameKey]
                                 );
            }
        }

        private static IEnumerable<string> DefaultLanguageKeys
        {
            get { return WebConfigurationManager.AppSettings[WebConfigDefaultLanguagesKey].Split(new[] {','}); }
        }

        private static Config Config
        {
            get
            {
                return new Config
                           {
                               DefaultLanguageKeys = DefaultLanguageKeys,
                               MaxPastesInRecentView =
                                   int.Parse(WebConfigurationManager.AppSettings[WebConfigMaxPastesInRecentViewKey]),
                               MaxPreviewCharacters =
                                   int.Parse(WebConfigurationManager.AppSettings[WebConfigMaxPreviewCharactersKey])
                           };
            }
        }
    }
}