using System;

namespace HS.PasteBin.Web.ViewModels
{
    public class RecentPasteInfo
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Preview { get; set; }
        public string LanguageName { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}, CreatedAt: {1}, Preview: {2}, LanguageName: {3}", ID, CreatedAt, Preview, LanguageName);
        }

        public bool Equals(RecentPasteInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.ID == ID && other.CreatedAt.Equals(CreatedAt) && Equals(other.Preview, Preview) && Equals(other.LanguageName, LanguageName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RecentPasteInfo)) return false;
            return Equals((RecentPasteInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = ID;
                result = (result*397) ^ CreatedAt.GetHashCode();
                result = (result*397) ^ (Preview != null ? Preview.GetHashCode() : 0);
                result = (result*397) ^ (LanguageName != null ? LanguageName.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(RecentPasteInfo left, RecentPasteInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RecentPasteInfo left, RecentPasteInfo right)
        {
            return !Equals(left, right);
        }
    }
}