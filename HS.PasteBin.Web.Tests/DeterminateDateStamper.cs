using System;
using HS.PasteBin.Model;

namespace HS.PasteBin.Web.Tests
{
    class DeterminateDateStamper : IDateStamper
    {
        private DateTime initialDateTime;

        public DeterminateDateStamper(DateTime initialDateTime)
        {
            this.initialDateTime = initialDateTime;
        }

        public DateTime Now
        {
            get
            {
                var currentDateTime = initialDateTime;
                initialDateTime = initialDateTime.AddMilliseconds(1);
                return currentDateTime;
            }
        }
    }
}
