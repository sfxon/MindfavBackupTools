// <copyright file="RotateBackupLogic.cs" company="Mindfav Software">
// Copyright (c) Mindfav Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace DomainLayer.BusinessLogic.Commands
{
    using System.Globalization;
    using System.IO.Abstractions;
    using DomainLayer.BusinessLogic.Configuration;
    using DomainLayer.BusinessLogic.Exceptions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// BusinessLogic for rotating backups.
    /// </summary>
    public class RotateBackupLogic
    {
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly IFileSystem? fileSystem = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotateBackupLogic"/> class.
        /// </summary>
        /// <param name="appConfig">Current application configuration.</param>
        /// <param name="fileSystem"><see cref="IFileSystem"/>.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public RotateBackupLogic(
            IOptions<ApplicationConfiguration> appConfig, 
            IFileSystem fileSystem)
        {
            this.applicationConfiguration = appConfig.Value;
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Rotates the backups.
        /// </summary>
        /// <param name="path">The full path of the folder, that holds the backup.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Rotate(string path)
        {
            this.CheckInitialization();

            List<string> folders = this.fileSystem!.Directory.GetDirectories(path).ToList();

            for (int i = 0; i < folders.Count; i++)
            {
                this.DeleteFolderIfNotInBackupRange(path, folders[i]);
            }

            return true;
        }

        private static bool IsFolderDateInFuture(DateTime folderDate)
        {
            if (folderDate > DateTime.Now)
            {
                return true;
            }

            return false;
        }

        private static bool IsDateWithinLastThreeDays(DateTime folderDate)
        {
            if (folderDate > DateTime.Now.AddDays(-3))
            {
                return true;
            }

            return false;
        }

        private static bool IsDateASundayInLastFourWeeks(DateTime folderDate)
        {
            if (folderDate < DateTime.Now.AddDays(-30))
            {
                return false;
            }

            if (folderDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            return false;
        }

        private static bool IsDateLastDayOfMonth(DateTime folderDate)
        {
            if (folderDate.Day == DateTime.DaysInMonth(folderDate.Year, folderDate.Month))
            {
                return true;
            }

            return false;
        }

        private void DeleteFolderIfNotInBackupRange(string path, string folder)
        {
            string realname = folder.Replace(path, string.Empty);

            // Fetch the date from folder
            DateTime folderDate = Convert.ToDateTime(
                realname,
                CultureInfo.CreateSpecificCulture(
                    this.applicationConfiguration.RotationFoldersDateCultureInfo));

            // Is the date in future?
            if (IsFolderDateInFuture(folderDate))
            {
                throw new FolderDateInFutureException();
            }

            // Is the date in last 3 days?
            if (IsDateWithinLastThreeDays(folderDate))
            {
                return;
            }

            // Is the date a sunday in past 4 weeks?
            if (IsDateASundayInLastFourWeeks(folderDate))
            {
                return;
            }

            // Is the date the last day of a month?
            if (IsDateLastDayOfMonth(folderDate))
            {
                return;
            }

            // Remove folder with files.
            this.fileSystem!.Directory.Delete(folder, true);

            return;
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