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
        /// Gets or sets the culuture info for the folders in the rotation.
        /// The folders that should be rotated are expected to be named by dates.
        /// With the culture info, you can set a different date-format.
        /// The default date is en-US.
        /// </summary>
        public string RotationFoldersDateCultureInfo { get; set; } = "en-US";
    }
}
