using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;
using HS.PasteBin.Core;
using HS.PasteBin.Model;
using HS.SqlServer;

namespace HS.PasteBin.Data
{
    public class SqlPasteRepository : IPasteRepository
    {
        private readonly IDateStamper dateStamper;
        private const string PasteMapResourcePath = "HS.PasteBin.Data.Paste.map";
        public ConnectionInfo ConnectionInfo { get; private set; }

        public SqlPasteRepository(ConnectionInfo connectionInfo, IDateStamper dateStamper)
        {
            this.dateStamper = dateStamper;
            ConnectionInfo = connectionInfo;
        }

        private PasteBin OpenPasteBin()
        {
            return new PasteBin
                (
                ConnectionInfo.DatabaseConnectionString,
                XmlMappingSource.FromStream(PasteMappingStream)
                );
        }

        private static Stream PasteMappingStream
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(PasteMapResourcePath);
            }
        }

        public Paste Find(int id)
        {
            using (var pasteBin = OpenPasteBin())
            {
                return pasteBin.Pastes.FirstOrDefault(paste => paste.Id == id);
            }
        }

        public int Create(string text, string html, string preview, string languageKey)
        {
            using (var pasteBin = OpenPasteBin())
            {
                var paste = new Paste
                                {
                                    Text = text,
                                    CreatedAt = dateStamper.Now,
                                    Language = languageKey,
                                    Html = html,
                                    Preview = preview,
                                };

                pasteBin.Pastes.InsertOnSubmit(paste);

                pasteBin.SubmitChanges();

                return paste.Id;
            }
        }

        public IEnumerable<Paste> Last(int count)
        {
            using (var pasteBin = OpenPasteBin())
            {
                return pasteBin.Pastes
                    .OrderByDescending(paste => paste.CreatedAt)
                    .Take(count)
                    .ToList();
            }
        }
    }

}
