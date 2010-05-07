using System;

namespace HS.PasteBin.Web.Exceptions
{
    public class LanguageUnknownException : Exception
    {
        public LanguageUnknownException(string languageKey)
            :   base("Unknown language: " + languageKey)
        {
            // Empty.
        }
    }
}