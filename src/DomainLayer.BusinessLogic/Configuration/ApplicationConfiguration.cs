// <copyright file="ApplicationConfiguration.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Configuration
{
    /// <summary>
    /// Class for the application configuration.
    /// </summary>
    /// <remarks>
    /// Provided by the dependency injection.
    /// </remarks>
    public sealed class ApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets path of the folder to rotate.
        /// </summary>
        public string BackupRotationPath { get; set; } = string.Empty;
    }
}
