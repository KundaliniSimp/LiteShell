﻿using System;
using System.Collections.Generic;

namespace CodeCraftersShell 
{
    class PathManager {

        readonly string[]? directories;
        readonly string? userHomeDir;

        public PathManager() {

            string? envPath = Environment.GetEnvironmentVariable(ShellConstants.ENV_VAR_PATH);

            if (envPath != null) {
                directories = envPath.Split(ShellConstants.ENV_PATH_SEPARATOR);
            }

            userHomeDir = Environment.GetEnvironmentVariable(ShellConstants.ENV_VAR_HOME);
        }

        public string? GetExecutablePath(string exeName) {

            if (directories == null) {
                return null;
            }

            foreach (string dir in directories) {
                string fullPath = $"{dir}{ShellConstants.ENV_DIR_SEPARATOR}{exeName}{ShellConstants.ENV_EXECUTABLE_EXT}";

                if (File.Exists(fullPath)) {
                    return fullPath;
                }
            }

            return null;
        }

        public string GetCurrentDir() {
            return Environment.CurrentDirectory;
        }

        public bool TrySetDir(string userDir) {

            if (userDir.Length == 1 && userDir[0] == ShellConstants.SYMB_HOME) {
                if (userHomeDir != null) {
                    Environment.CurrentDirectory = userHomeDir;
                    return true;
                }
                else {
                    return false;
                }
            }

            if (Directory.Exists(userDir)) {
                Environment.CurrentDirectory = userDir;
                return true;
            }

            return false;
        }
    } 
}