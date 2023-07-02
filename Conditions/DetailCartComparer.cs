using CloudComputing.Models;
using System.Diagnostics.CodeAnalysis;

namespace CloudComputing.Conditions
{
    public class DetailCartComparer : IEqualityComparer<DetailCart>
    {
        public bool Equals(DetailCart? x, DetailCart? y)
        {
            return x.IdSp.Trim() == y.IdSp.Trim();
        }

        public int GetHashCode([DisallowNull] DetailCart obj)
        {
            return obj.IdSp.GetHashCode();
        }
    }
}
