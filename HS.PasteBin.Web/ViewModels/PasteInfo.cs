using System;

namespace HS.PasteBin.Web.ViewModels
{
    public class PasteInfo
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Html { get; set; }
        public string Language { get; set; }

        public bool Equals(PasteInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.ID == ID && other.CreatedAt.Equals(CreatedAt) && Equals(other.Html, Html) && Equals(other.Language, Language);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PasteInfo)) return false;
            return Equals((PasteInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = ID;
                result = (result*397) ^ CreatedAt.GetHashCode();
                result = (result*397) ^ (Html != null ? Html.GetHashCode() : 0);
                result = (result*397) ^ (Language != null ? Language.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(PasteInfo left, PasteInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PasteInfo left, PasteInfo right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("ID: {0}, CreatedAt: {1}, Html: {2}, Language: {3}", ID, CreatedAt, Html, Language);
        }
    }
}