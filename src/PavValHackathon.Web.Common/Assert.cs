using System;

namespace PavValHackathon.Web.Common
{
    public static class Assert
    {
        public static void IsNotNull<TObj>(TObj obj, string name)
            where TObj : class
        {
            if (obj is null) throw new ArgumentNullException(name);
        }
        
        public static void IsGreaterThanZero(int value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(parameterName, $@"{parameterName} can not be less than 1.");
        }
    }
}