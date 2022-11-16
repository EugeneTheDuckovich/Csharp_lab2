using System.Diagnostics.CodeAnalysis;
using lab2_src.Model;

namespace lab2_src.EqualityComparers;

internal class PortEqualityComparer : IEqualityComparer<IPort>
{
    public bool Equals(IPort? x, IPort? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] IPort obj)
    {
        return obj.GetHashCode();
    }
}
