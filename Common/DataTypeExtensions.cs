using System;

namespace Common
{

    public static class DataTypeExtensions
    {
        public static int ToInt(this string source)
        {
            int result;
            int.TryParse(source, out result);
            return result;
        }
        public static int ToInt(this string source, int defaultValue)
        {
            int result;
            if(!int.TryParse(source, out result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}
