using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroXmlSerializer
{
    static class Extensions
    {
        public static TAttribute Find<TAttribute>(this IEnumerable<Attribute> attributes)
            where TAttribute : Attribute
        {
            return attributes.OfType<TAttribute>().FirstOrDefault();
        }
    }
}
