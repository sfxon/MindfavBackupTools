// <copyright file="SetCommand.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Application.CommandLine.Commands
{
    using System;
    using System.Globalization;
    using Application.CommandLine.Tools;
    using DomainLayer.BusinessLogic.Commands;
    using DomainLayer.BusinessLogic.Exceptions;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Command for writing settings.
    /// </summary>
    public class SetCommand
    {
        /// <summary>
        /// Command to save a setting.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider" />.</param>
        /// <param name="args">Array of type string, which contains the commandline parameters.</param>
        /// <returns>True, if successful, false otherwise.</returns>
        public static bool Set(IServiceProvider serviceProvider, string[] args)
        {
            // Check, if the required command line parameters are given.
            if (
                args.Length != 3 ||
                string.IsNullOrEmpty(args[1]) ||
                string.IsNullOrEmpty(args[2]))
            {
                SetCommand.PrintUsageError();
                return false;
            }

            // Call business logic to write the setting.
            var setLogic = serviceProvider.GetRequiredService<SetLogic>();

            try
            {
                setLogic.SaveConfiguration(args[1], args[2]);
            }
            catch (InvalidOptionKeyException)
            {
                SetCommand.PrintInvalidOptionError();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Print error about wrong usage of the command.
        /// </summary>
        public static void PrintUsageError()
        {
            Cmd.PrintError(Messages.SetCommandUsageError);
            PrintUsageInfo();
        }

        /// <summary>
        /// Print error about invalid options.
        /// </summary>
        public static void PrintInvalidOptionError()
        {
            Cmd.PrintError(Messages.SetCommandInvalidOptionError);
            PrintUsageInfo();
        }

        /// <summary>
        /// Print information about the usage of the program.
        /// </summary>
        public static void PrintUsageInfo()
        {
            Cmd.Print(
                string.Format(
                    CultureInfo.InvariantCulture,
                    Messages.SetCommandUsageTitle,
                    Messages.SetCommandUsage));
        }
    }
}
