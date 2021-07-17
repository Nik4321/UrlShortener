using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UrlShortener.Infrastructure
{
    /// <summary>
    ///  Static helper class for common argument assertions and exceptions
    /// </summary>
    public static class AssertArgument
    {
        /// <summary>
        /// Asserts that the argument value is not null, throws and <see cref="ArgumentNullException"/> if it is.
        /// </summary>
        /// <param name="argumentValue">The value of the argument</param>
        /// <param name="argumentName">The name of the argument</param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public static void NotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Asserts that the string argument is not null, empty or white space; throws an Exception if it is.
        /// </summary>
        /// <param name="argumentValue">The value of the argument</param>
        /// <param name="argumentName">The name of the argument</param>
        /// <param name="message">Optional message to include in an <see cref="ArgumentNullException"/> if one is thrown</param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        /// <exception cref="ArgumentException">Thrown if the argument is empty or white space</exception>
        public static void NotNullOrWhiteSpace(string argumentValue, string argumentName, string message = null)
        {
            NotNull(argumentValue, argumentName);

            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                throw new ArgumentException(message ?? "Cannot be empty or white space", argumentName);
            }
        }

        /// <summary>
        /// Asserts that the IEnumerable argument is not null or empty, throws an Exception if it is
        /// </summary>
        /// <param name="argumentValue">The value of the argument</param>
        /// <param name="argumentName">The name of the argument</param>
        /// <param name="message">The message to include in the Exception if one is thrown</param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        /// <exception cref="ArgumentException">Thrown if the argument is empty</exception>
        public static void NotNullOrEmpty(IEnumerable argumentValue, string argumentName, string message = null)
        {
            NotNull(argumentValue, argumentName);

            var isEmpty = argumentValue switch
            {
                IEnumerable<object> enumerableOfAny => !enumerableOfAny.Any(),
                _ => !argumentValue.GetEnumerator().MoveNext()
            };

            if (isEmpty)
            {
                throw new ArgumentException(message ?? "Cannot be empty", argumentName);
            }
        }
    }
}
