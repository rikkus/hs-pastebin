using System.Collections.Generic;
using HS.PasteBin.Core;

namespace HS.PasteBin.Model
{
    public interface IPasteRepository
    {
        Paste Find(int id);
        int Create(string text, string html, string preview, string languageKey);
        IEnumerable<Paste> Last(int count);
    }
}