﻿/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2022 DUONG DIEU PHAP
Project homepage: https://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using ImageGlass.Base.Update;
using ImageGlass.Settings;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace igcmd {
    public static class Core {
        /// <summary>
        /// Check for update
        /// </summary>
        public static void AutoUpdate() {
            _ = AutoUpdateAsync();
        }


        private static async Task AutoUpdateAsync() {
            var updater = new UpdateService();

            try {
                await updater.GetUpdatesAsync();
            }
            catch { }

            Configs.IsNewVersionAvailable = updater.HasNewUpdate;

            if (updater.HasNewUpdate) {
                Application.Run(new frmCheckForUpdate());
            }
        }


        /// <summary>
        /// Check for update
        /// </summary>
        public static bool CheckForUpdate() {
            Application.Run(new frmCheckForUpdate());

            return Configs.IsNewVersionAvailable;
        }
    }
}
