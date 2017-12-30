using System;

namespace uSharpBrowser.Utilities
{
    public static class Guard
    {
        public static T ArgumentNotNull<T>(T value, string parameterName) where T : class
        {
            if (value is null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static void ArgumentNotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }
        }

        private static string NotEmpty(string value, string parameterName)
        {
            Exception e = null;
            if (value is null)
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException(parameterName);
            }

            if (e != null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw e;
            }
            return value;
        }

    }
}
