using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Model.Util
{
    public static class Extensions
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
        {
            return source.Where(x => x != null)!;
        }
    }
}