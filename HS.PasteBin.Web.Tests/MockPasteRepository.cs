using System;
using System.Collections.Generic;
using System.Linq;
using HS.PasteBin.Core;
using HS.PasteBin.Model;

namespace HS.PasteBin.Web.Tests
{
    internal class MockPasteRepository : IPasteRepository
    {
        private readonly IDateStamper dateStamper;
        private readonly Dictionary<int, Paste> pastes = new Dictionary<int, Paste>();

        public MockPasteRepository(IEnumerable<Paste> pastes, IDateStamper dateStamper)
        {
            this.dateStamper = dateStamper;

            foreach (Paste paste in pastes)
            {
                this.pastes.Add(paste.Id, paste);
            }
        }

        public Paste Find(int id)
        {
            return pastes[id];
        }

        public int Create(string text, string html, string preview, string languageKey)
        {
            int key = NextFreeKey();

            pastes.Add
                (
                key,
                new Paste
                    {
                        CreatedAt = dateStamper.Now,
                        Html = html,
                        Id = key,
                        Language = languageKey,
                        Preview =
                            preview,
                        Text = text
                    }
                );

            return key;
        }

        public IEnumerable<Paste> Last(int count)
        {
            return pastes.Values.OrderByDescending(paste => paste.CreatedAt).Take(count);
        }

        private int NextFreeKey()
        {
            return pastes.Keys.Max() + 1;
        }
    }
}