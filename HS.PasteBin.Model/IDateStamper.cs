using System;

namespace HS.PasteBin.Model
{
    public interface IDateStamper
    {
        DateTime Now { get; }
    }
}
