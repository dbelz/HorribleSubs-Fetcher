using System;
using System.Diagnostics;

namespace HorribleSubsFetcher
{
    internal static class Argument
    {
        /// <summary>
        /// Throws a <see cref="ArgumentNullException"/> if the parameter is null.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="message">The <see cref="ArgumentNullException"/> exception message.</param>
        [DebuggerStepThrough]
        internal static void NotNull(
            object param,
            string paramName = "",
            string message = "")
        {
            if (param == null)
                throw new ArgumentNullException(paramName, message);
        }

        /// <summary>
        /// Throws a <see cref="ArgumentNullException"/> if the parameter is null. If the parameter
        /// is a whitespace it throws a <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="message">The exception message.</param>
        [DebuggerStepThrough]
        internal static void NotNullOrWhiteSpace(
            string param,
            string paramName = "",
            string message = "")
        {
            NotNull(param, paramName, message);

            if (string.IsNullOrWhiteSpace(param))
                throw new ArgumentException(message, paramName);
        }
    }
}
