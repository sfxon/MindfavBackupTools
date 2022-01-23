// <copyright file="SetLogic.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using DomainLayer.BusinessLogic.Configuration;
    using DomainLayer.BusinessLogic.Exceptions;

    /// <summary>
    /// Implementation of the command line command to write a setting.
    /// </summary>
    public class SetLogic
    {
        private readonly ConfigurationWriter configurationWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetLogic"/> class.
        /// </summary>
        /// <param name="configWriter"><see cref="ConfigurationWriter"/>.</param>
        public SetLogic(
            ConfigurationWriter configWriter)
        {
            this.configurationWriter = configWriter;
        }

        /// <summary>
        /// Writes a setting.
        /// </summary>
        /// <param name="key">Name of the setting, that should be written.</param>
        /// <param name="value">Value of the setting, that should be written..</param>
        public void SaveConfiguration(string key, string value)
        {
            // Check, that a valid key is provided.
            if (!KeyIsValidOption(key))
            {
                throw new InvalidOptionKeyException();
            }

            string? realKeyName = GetRealKeyName(key);

            // Microsoft says, that settings should be save insensitive.
            // We do it like that.
            this.configurationWriter.Write<string>(
                AppDomain.CurrentDomain.BaseDirectory + "/appsettings.json",
                "AppSettings",
                realKeyName!,
                value);
        }

        /// <summary>
        /// Fetches all setting-names from <see cref="ApplicationConfiguration"/>.
        /// </summary>
        /// <returns>A dictionary with index and value of type string.
        /// The index in the dictionary is the name of the property in lowercase letters,
        /// the value is the name of the property in correct case.</returns>
        private static Dictionary<string, string> GetAllPropertiesFromConfigClass()
        {
            PropertyInfo[] properties = typeof(ApplicationConfiguration).GetProperties();
            var propertyDictionary = new Dictionary<string, string>();

            // Application is working case-insensitive in the options of the json file,
            // like Microsoft does. Because of that, we build a map,
            // to intermediate between user-inputs and the data in the settings object.
            foreach (PropertyInfo property in properties)
            {
                propertyDictionary.Add(
                    property.Name.ToLower(),
                    property.Name);
            }

            return propertyDictionary;
        }

        /// <summary>
        /// Checks, if a key is a property in <see cref="ApplicationConfiguration"/>.
        /// Ignores uppercase/lowercase (is case-insensitive).
        /// </summary>
        /// <param name="key">Key, that should be checked for.</param>
        /// <returns>True, if the key exists, false otherwise.</returns>
        private static bool KeyIsValidOption(string key)
        {
            var availableProperties = GetAllPropertiesFromConfigClass();

            key = key.ToLower();

            if (availableProperties.ContainsKey(key))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fetches the name in real case from <see cref="ApplicationConfiguration"/>.
        /// </summary>
        /// <param name="key">Schlüssel, that should be returned.</param>
        /// <returns>String with name in correct case. Null, if the property does not
        /// exist in <see cref="ApplicationConfiguration"/>.</returns>
        private static string? GetRealKeyName(string key)
        {
            var availableProperties = GetAllPropertiesFromConfigClass();

            key = key.ToLower();

            if (availableProperties.ContainsKey(key))
            {
                return availableProperties[key];
            }

            return null;
        }
    }
}
