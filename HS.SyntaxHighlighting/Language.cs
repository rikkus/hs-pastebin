using System;

namespace HS.SyntaxHighlighting
{
    public class Language : IEquatable<Language>
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public Language(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Language)) return false;
            return Equals((Language) obj);
        }

        public static bool operator ==(Language left, Language right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Language left, Language right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key != null ? Key.GetHashCode() : 0)*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public bool Equals(Language other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Key, Key) && Equals(other.Name, Name);
        }

        public override string ToString()
        {
            return string.Format("Key: {0}, Name: {1}", Key, Name);
        }
    }
}