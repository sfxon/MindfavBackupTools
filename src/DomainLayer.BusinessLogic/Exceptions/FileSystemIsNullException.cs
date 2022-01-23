// <copyright file="FileSystemIsNullException.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception for the case, that the file system is null.
    /// </summary>
    public class FileSystemIsNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemIsNullException"/> class.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        public FileSystemIsNullException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemIsNullException"/> class.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="innerException">Encapsuled exception of type <see cref="Exception"/>.</param>
        public FileSystemIsNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemIsNullException"/> class.
        /// </summary>
        public FileSystemIsNullException()
            : base()
        {
        }
    }
}
