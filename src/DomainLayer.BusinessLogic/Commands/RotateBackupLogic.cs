// <copyright file="RotateBackupLogic.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Commands
{
    using System.IO.Abstractions;

    /// <summary>
    /// BusinessLogic for rotating backups.
    /// </summary>
    public class RotateBackupLogic
    {
        private readonly IFileSystem? fileSystem = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateBackupLogic"/> class.
        /// </summary>
        /// <param name="fileSystem"><see cref="IFileSystem"/>.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public RotateBackupLogic(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Rotates the backups.
        /// </summary>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Rotate()
        {
            return true;
        }
    }
}