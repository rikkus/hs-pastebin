using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using HS.Migration;
using HS.PasteBin.Migration;
using HS.SqlServer;

namespace HS.PasteBin.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ConnectionInfo connectionInfo;

        public AdminController(ConnectionInfo connectionInfo)
        {
            this.connectionInfo = connectionInfo;
        }

        public ActionResult Install()
        {
            try
            {
                using (var connection = new SqlConnection(connectionInfo.DatabaseConnectionString))
                {
                    connection.Open();

                    var versionedDb = new SqlVersionedDb(connection);
                    var migrationStorage = new AssemblyMigrationStorage(typeof (Locator).Assembly);

                    new Migrator(versionedDb, migrationStorage)
                        .MigrateToNewestVersion(TransactionPolicy.RollBackOnError);

                    ViewData["DatabaseVersion"] = versionedDb.Version();
                }

                ViewData["Message"] = Properties.Resources.CreatePageTitle;
                ViewData["Result"] = true;
            }
            catch (Exception ex)
            {
                ViewData["DatabaseVersion"] = decimal.Zero;
                ViewData["Message"] = ex.Message;
                ViewData["Result"] = false;
            }

            return View();
        }
    }
}