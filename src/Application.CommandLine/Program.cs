// <copyright file="Program.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Application.CommandLine
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Reflection;
    using Application.CommandLine.Tools;
    using DomainLayer.BusinessLogic.Commands;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Application startup class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application startup method.
        /// </summary>
        /// <param name="args">Array of type string, which contains the commandline parameters.</param>
        /// /// <returns>A value, like defined in <see cref="ResultCode"/>.</returns>
        public static int Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();

            if (IsHelpCommandLine(args))
            {
                PrintUsage();
                return (int)ResultCode.Ok;
            }

            if (args.Length >= 1 && !string.IsNullOrEmpty(args[0]))
            {
                // Initialize the list of available commands.
                var commands = new List<Command>
                {
                    new Command("rotate", RotateCommand),
                };

                // Suche den Befehl anhand des übergebenen Parameters.
                var command = commands.SingleOrDefault(c =>
                    string.Compare(c.Name, args[0], StringComparison.OrdinalIgnoreCase) == 0);

                // Execute the command..
                if (command != null)
                {
                    // Die Ausführung in eine Transaktion fassen.
                    var result = command.Handler.Invoke(serviceProvider, args);

                    if (result == ResultCode.WrongCommandLine)
                    {
                        PrintUsage();
                    }

                    return (int)result;
                }
            }

            // When we get here, no command has been executed. Print usage and exit.
            PrintUsage();

            return (int)ResultCode.WrongCommandLine;
        }

        /// <summary>
        /// Initialize dependency injection with services.
        /// </summary>
        /// <returns><see cref="IServiceProvider"/>.</returns>
        private static IServiceProvider CreateServiceProvider()
        {
            IServiceProvider serviceProvider = null;

            var fileSystem = new FileSystem();
            var configuration = GetConfiguration();

            var services = new ServiceCollection();

            services.AddSingleton<IFileSystem>(fileSystem);

            /*
            services.Configure<ApplicationConfiguration>(
                options => configuration.GetSection("AppSettings").Bind(options));
            */
            services.AddTransient<RotateBackupLogic>();

            /*
            services.AddTransient<ConfigurationWriter>();
            services.AddTransient<SetLogic>();
            services.AddTransient<ChangelogLogic>();

            services.AddTransient<IVersionSystem, Git>();
            services.AddTransient<ITicketSystem, Mantis>();
            */

            serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        /// Fetches the application configuration.
        /// </summary>
        /// <returns><see cref="IConfigurationRoot"/>.</returns>
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            builder.AddJsonFile("appsettings.json", optional: true);
            return builder.Build();
        }

        /// <summary>
        /// Checks, if a string contains a marker for a help output.
        /// </summary>
        /// <param name="args">Array of type string, which contains the commandline parameters.</param>
        /// <returns>True, if a marker was found. False, if no marker was found.</returns>
        private static bool IsHelpCommandLine(string[] args)
        {
            if (args == null || args.Length != 1 || string.IsNullOrEmpty(args[0]))
            {
                return false;
            }

            string argument = args[0].ToUpperInvariant();

            if (argument == "-?" || argument == "/?" || argument == "?" ||
                argument == "--HELP" || argument == "/HELP" || argument == "HELP")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Prints the usage of the program on the commandline.
        /// </summary>
        private static void PrintUsage()
        {
            string applicationName = Assembly.GetEntryAssembly()?.GetName().Name;
            Cmd.Print(Messages.Usage);
        }

        /// <summary>
        /// Command to rotate a backup.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/>.</param>
        /// <param name="args">Array of type string, which contains the commandline parameters.</param>
        /// <returns><see cref="ResultCode"/>.</returns>
        private static ResultCode RotateCommand(IServiceProvider serviceProvider, string[] args)
        {
            if (Commands.RotateCommand.Rotate(serviceProvider, args))
            {
                return ResultCode.Ok;
            }
            else
            {
                return ResultCode.Error;
            }
        }
    }
}
