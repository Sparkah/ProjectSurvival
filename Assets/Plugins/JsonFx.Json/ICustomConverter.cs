using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonFx.Json
{
    public interface ICustomConverter
    {
        object TryConvert(object value, Type targetType);
        Type TryConvertType(Type initialType);
        Type ArrayConversion(Type initialType);
    }
}
