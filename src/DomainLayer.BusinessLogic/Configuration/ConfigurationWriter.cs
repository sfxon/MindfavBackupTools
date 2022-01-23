// <copyright file="ConfigurationWriter.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Configuration
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using DomainLayer.BusinessLogic.Exceptions;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>
    /// Ermöglicht das Schreiben von Konfigurationsdaten in Dateien.
    /// </summary>
    public class ConfigurationWriter
    {
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly IFileSystem? fileSystem = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationWriter"/> class.
        /// </summary>
        /// <param name="appConfig">Current application configuration.</param>
        /// <param name="fileSystem">Unit-testable class for the file system.</param>
        public ConfigurationWriter(
            IOptions<ApplicationConfiguration> appConfig,
            IFileSystem fileSystem)
        {
            this.applicationConfiguration = appConfig.Value;
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Write a setting in the defined settings file,
        /// which must be in json format.
        /// </summary>
        /// <typeparam name="T">Type of element, that has to be written.</typeparam>
        /// <param name="filePath">Path to the configuration file, that should be updated.</param>
        /// <param name="section">Section, that receives the setting.</param>
        /// <param name="key">Name of the setting, that should be written.</param>
        /// <param name="value">The value of the setting, that should be written.</param>
        public void Write<T>(
            string filePath,
            string section,
            string key,
            T value)
        {
            this.CreateConfigFileIfNotExists(filePath);

            string jsonString = this.fileSystem!.File.ReadAllText(filePath);
            dynamic? jsonObj = JsonConvert.DeserializeObject(jsonString);

            if (jsonObj != null)
            {
                jsonObj[section][key] = value;

                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                this.fileSystem.File.WriteAllText(filePath, output);
            }
            else
            {
                throw new ConfigurationJsonDataInvalidException();
            }
        }

        /// <summary>
        /// Creates a configuration file with current settings,
        /// if it does'nt exist.
        /// </summary>
        /// <param name="filePath">Path to the configuration file, that should be written.</param>
        public void CreateConfigFileIfNotExists(string filePath)
        {
            this.CheckInitialization();

            // Create an object, that reflects the json configuration.
            IDictionary<string, ApplicationConfiguration> layout =
                new Dictionary<string, ApplicationConfiguration>
                {
                    { "AppSettings", this.applicationConfiguration },
                };

            // Objekt im JSON Format in Datei schreiben.
            string output = JsonConvert.SerializeObject(
                layout,
                Formatting.Indented);
            this.fileSystem!.File.WriteAllText(filePath, output);
        }

        private void CheckInitialization()
        {
            if (this.fileSystem == null)
            {
                throw new FileSystemIsNullException();
            }
        }
    }
}
