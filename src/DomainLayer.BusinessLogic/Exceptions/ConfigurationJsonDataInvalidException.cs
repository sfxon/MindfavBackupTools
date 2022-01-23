// <copyright file="ConfigurationJsonDataInvalidException.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception for the case, that the json configuration is invalid.
    /// </summary>
    public class ConfigurationJsonDataInvalidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationJsonDataInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        public ConfigurationJsonDataInvalidException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationJsonDataInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="innerException">Encapsuled exception of type <see cref="Exception"/>.</param>
        public ConfigurationJsonDataInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationJsonDataInvalidException"/> class.
        /// </summary>
        public ConfigurationJsonDataInvalidException()
            : base()
        {
        }
    }
}
