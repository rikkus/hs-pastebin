using System;

namespace HS.SyntaxHighlighting
{
    public class HighlightException : Exception
    {
        public HighlightException(string message)
            :   base(message)
        {
            // Empty.
        }

        public HighlightException(string message, Exception innerException)
            :   base(message, innerException)
        {
            // Empty.
        }
    }
}
