using System.Collections.Generic;

namespace HS.PasteBin.Web
{
    public class Config
    {
        public IEnumerable<string> DefaultLanguageKeys { get; set; }
        public int MaxPreviewCharacters { get; set; }
        public int MaxPastesInRecentView { get; set; }
    }
}
