using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnjMedical.Shared.Extensions;

public static class ValidationExtension
{
    public static bool IsNullOrEmpty(this string source)
    {
        return String.IsNullOrEmpty(source);
    }

    public static bool IsNotNull(this string source)
    {
        return !String.IsNullOrEmpty(source);
    }

    public static bool IsNull(this object source)
    {
        return source is null;
    }

    public static bool IsNotNull(this object source)
    {
        return source is not null;
    }
}
