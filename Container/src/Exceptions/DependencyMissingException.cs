using System;
using System.Globalization;

namespace Unity.Exceptions
{
    /// <summary>
    /// Represents that a dependency could not be resolved.
    /// </summary>
    public class DependencyMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyMissingException"/> class with no extra information.
        /// </summary>
        public DependencyMissingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyMissingException"/> class with the given message.
        /// </summary>
        /// <param name="message">Some random message.</param>
        public DependencyMissingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="DependencyMissingException"/> class with the given
        /// message and inner exception.
        /// </summary>
        /// <param name="message">Some random message</param>
        /// <param name="innerException">Inner exception.</param>
        public DependencyMissingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyMissingException"/> class with the build key of the object begin built.
        /// </summary>
        /// <param name="buildKey">The build key of the object begin built.</param>
        public DependencyMissingException(object buildKey)
            : base(string.Format(CultureInfo.CurrentCulture,
                Error.MissingDependency,
                buildKey))
        {
        }
    }
}
