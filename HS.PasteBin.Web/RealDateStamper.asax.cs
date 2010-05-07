using System;
using HS.PasteBin.Model;

namespace HS.PasteBin.Web
{
    internal class RealDateStamper : IDateStamper
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}