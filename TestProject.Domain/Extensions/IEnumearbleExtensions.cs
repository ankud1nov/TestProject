using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Domain.Extensions
{
    public static class IEnumearbleExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> value)
        {
            return value == null ? new List<T>() : value;
        }
    }
}
