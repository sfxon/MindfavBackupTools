// <copyright file="RotateCommand.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Application.CommandLine.Commands
{
    using System;
    using Application.CommandLine.Tools;
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
            if (
                args.Length != 2 ||
                string.IsNullOrEmpty(args[1]))
            {
                RotateCommand.PrintUsageError();
                return false;
            }

            string path = args[1];

            // Falls ja -> Einstellung über Config Writer schreiben.
            var rotateBackupLogic = serviceProvider.GetRequiredService<RotateBackupLogic>();
            return rotateBackupLogic.Rotate(path);
        }

        /// <summary>
        /// Print error about wrong usage of the program.
        /// </summary>
        public static void PrintUsageError()
        {
            Cmd.PrintError(Messages.RotateCommandUsageError);
        }
    }
}
