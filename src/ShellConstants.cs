﻿namespace CodeCraftersShell
{
    static class ShellConstants
    {
        public const char SYMB_HOME = '~';
        public const char SYMB_QUOTE_SINGLE = '\'';
        public const char SYMB_QUOTE_DOUBLE = '"';
        public const char SYMB_ESCAPE = '\\';
        public const char SYMB_DOLLAR = '$';
        public const char SYMB_WHITESPACE = (char)32;
        public const char SYMB_NEWLINE = '\n';
        public const char SYMB_ESCAPABLE_N = 'n';
        public const char SYMB_BELL = '\a';
        public const string FLAG_REDIRECT_OUTPUT_DEFAULT = ">";
        public const string FLAG_REDIRECT_OUTPUT_NEW = "1>";
        public const string FLAG_REDIRECT_OUTPUT_APPEND_DEFAULT = ">>";
        public const string FLAG_REDIRECT_OUTPUT_APPEND = "1>>";
        public const string FLAG_REDIRECT_ERROR_NEW = "2>";
        public const string FLAG_REDIRECT_ERROR_APPEND = "2>>";
        public const string NEW_PROMPT = "$ ";
        public const string APP_TITLE = "LiteShell";
        public const string CMD_ECHO = "echo";
        public const string CMD_EXIT = "exit";
        public const string CMD_TYPE = "type";
        public const string CMD_PWD = "pwd";
        public const string CMD_CD = "cd";
        public const string CMD_HIST = "history";
        public const string CMD_CAT = "cat";
        public const string CMD_LS = "ls";
        public const string CMD_CLEAR = "clear";
        public const string RESP_INVALID_CMD = "command not found";
        public const string RESP_VALID_TYPE = "is a shell builtin";
        public const string RESP_INVALID_TYPE = "not found";
        public const string RESP_VALID_PATH = "is";
        public const string RESP_INVALID_DIR = "No such file or directory";
        public const string AUTOCOMPLETION_SEPARATOR = "  ";
        public const string ENV_VAR_PATH = "PATH";
        public const string ENV_VAR_HOME = "HOME";
        public const string HIST_LEFT_TAB = "    ";
        public const string HIST_MIDDLE_TAB = "  ";
        public const int SLEEP_INTERVAL = 10;

        public static readonly int INPUT_BUFFER_START = NEW_PROMPT.Length;

        public static readonly bool IS_WINDOWS = ShellUtilities.IsEnvironmentWindows();
        public static readonly char ENV_DIR_SEPARATOR = IS_WINDOWS ? '\\' : '/';
        public static readonly char ENV_PATH_SEPARATOR = IS_WINDOWS ? ';' : ':';
        public static readonly string ENV_EXECUTABLE_EXT = IS_WINDOWS ? ".exe" : "";

        public static readonly HashSet<string> BUILTINS = new([CMD_ECHO, CMD_EXIT, CMD_TYPE, CMD_PWD, CMD_CD, CMD_HIST, /*CMD_CAT, CMD_LS,*/ CMD_CLEAR]);
        public static readonly HashSet<char> SYMB_QUOTES = new([SYMB_QUOTE_SINGLE, SYMB_QUOTE_DOUBLE]);
        public static readonly HashSet<char> ESCAPABLES = new(
            [SYMB_WHITESPACE, SYMB_ESCAPE, SYMB_QUOTE_SINGLE, SYMB_QUOTE_DOUBLE, SYMB_NEWLINE, SYMB_ESCAPABLE_N]
        );
        public static readonly HashSet<char> DOUBLE_QUOTE_ESCAPABLES = new([SYMB_ESCAPE, SYMB_DOLLAR, SYMB_QUOTE_DOUBLE]);
    }
}
