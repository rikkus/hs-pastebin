using System;

namespace HS.PasteBin.Web.Exceptions
{
    public class PasteNotFoundException : Exception
    {
        public PasteNotFoundException(int id)
            :   base(string.Format("Paste with id {0} not found", id))
        {
            // Empty.
        }
    }
}