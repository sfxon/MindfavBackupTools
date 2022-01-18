// <copyright file="RotateCommand.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Application.CommandLine.Commands
{
    using System;
    using DomainLayer.BusinessLogic.Commands;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Command Handler to rotate backups.
    /// Passes the commandline command to the business logic.
    /// </summary>
    public class RotateCommand
    {
        /// <summary>
        /// Command to rotate backups.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/>.</param>
        /// <param name="args">Array of type string, which contains the commandline parameters.</param>
        /// <returns>True, if everything went well, otherwise false.</returns>
        public static bool Rotate(IServiceProvider serviceProvider, string[] args)
        {
            // Prüfen, ob die benötigten Kommandozeilenparameter angegeben wurden.
            /*
            if (
                args.Length != 3 ||
                string.IsNullOrEmpty(args[1]) ||
                string.IsNullOrEmpty(args[2]))
            {
                SetCommand.PrintUsageError();
                return false;
            }
            */

            // Falls ja -> Einstellung über Config Writer schreiben.
            var rotateBackupLogic = serviceProvider.GetRequiredService<RotateBackupLogic>();
            return rotateBackupLogic.Rotate();
        }
    }
}
