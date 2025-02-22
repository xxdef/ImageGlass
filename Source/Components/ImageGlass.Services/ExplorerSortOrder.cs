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

Author: Kevin Routley (aka fire-eggs)
*/
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ImageGlass.Base;

namespace ImageGlass.Services {
    public static class ExplorerSortOrder {
        /// <summary>
        /// Convert an Explorer column name to one of our currently available sorting orders.
        /// </summary>
        private static readonly Dictionary<string, ImageOrderBy> SortTranslation = new()
        {
            { "System.DateModified", ImageOrderBy.LastWriteTime },
            { "System.ItemDate", ImageOrderBy.LastWriteTime },
            { "System.ItemTypeText", ImageOrderBy.Extension },
            { "System.FileExtension", ImageOrderBy.Extension },
            { "System.FileName", ImageOrderBy.Name },
            { "System.ItemNameDisplay", ImageOrderBy.Name },
            { "System.Size", ImageOrderBy.Length },
            { "System.DateCreated", ImageOrderBy.CreationTime },
            { "System.DateAccessed", ImageOrderBy.LastAccessTime },
        };

        [DllImport("ExplorerSortOrder.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "GetExplorerSortOrder")]
        private static extern int GetExplorerSortOrder(string folderPath, ref StringBuilder columnName, int columnNameMaxLen, ref int isAscending);


        /// <summary>
        /// <para>
        /// Determines the sorting order of a Windows Explorer window which matches
        /// the given file path.
        /// </para>
        /// <para>
        /// "Failure" situations are:
        /// 1. unable to find an open Explorer window matching the file path
        /// 2. the Explorer sort order doesn't match one of our existing sort orders
        /// </para>
        /// </summary>
        /// <param name="fullPath">full path to file/folder in question</param>
        /// <param name="loadOrder">the resulting sort order or null</param>
        /// <param name="isAscending">the resulting sort direction or null</param>
        /// <returns>false on failure - out parameters will be null!</returns>
        public static bool GetExplorerSortOrder(string fullPath, out ImageOrderBy? loadOrder, out bool? isAscending) {
            // assume failure
            loadOrder = null;
            isAscending = null;

            try {
                // if fullPath is a drive root (e.g. "L:\") then Path.GetDirectoryName returns null
                var folderPath = Path.GetDirectoryName(fullPath) ?? fullPath;

                var sb = new StringBuilder(200); // arbitrary length should fit any
                int sortResult;
                var ascend = -1;

                sortResult = GetExplorerSortOrder(folderPath, ref sb, sb.Capacity, ref ascend);

                if (sortResult != 0) // failure
                {
                    return false;
                }

                // Success! Attempt to translate the Explorer column to our supported
                // sort order values.
                var column = sb.ToString();
                if (SortTranslation.ContainsKey(column)) {
                    loadOrder = SortTranslation[column];
                }

                isAscending = ascend > 0;

                return loadOrder != null; // will be false on not-yet-supported column
            }
            catch {
                return false; // failure
            }
        }
    }
}
