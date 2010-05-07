using HS.PasteBin.Core;

namespace HS.PasteBin.Data
{
    public partial class PasteBin : System.Data.Linq.DataContext
    {
        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertPaste(Paste instance);
        partial void UpdatePaste(Paste instance);
        partial void DeletePaste(Paste instance);
        #endregion

        public PasteBin(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public PasteBin(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public System.Data.Linq.Table<Paste> Pastes
        {
            get
            {
                return GetTable<Paste>();
            }
        }
    }
}