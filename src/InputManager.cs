﻿using System.Text;

namespace CodeCraftersShell
{
    class InputManager
    {
        
        enum CursorDirection {
            LEFT = -1,
            RIGHT = 1,
        }

        HashSet<string> autocompletionMatches = new();
        StringBuilder inputBuffer;
        string[] autocompletionCache;

        public InputManager(string[]? executables) {

            inputBuffer = new();
            autocompletionCache = Array.Empty<string>();

            foreach (string builtin in ShellConstants.BUILTINS) {
                autocompletionMatches.Add(builtin);
            }

            if (executables != null) {
                foreach (string exe in executables) {
                    autocompletionMatches.Add(exe);
                }
            }
        }

        public string? GetUserInput() {

            bool isReading = true;

            while (isReading) {

                string writeBuffer = "";
                ConsoleKeyInfo currentKey = Console.ReadKey(true);

                switch (currentKey.Key) {
                    case ConsoleKey.Enter: isReading = false; break;
                    case ConsoleKey.LeftArrow: MoveCursor(CursorDirection.LEFT, inputBuffer.Length); break;
                    case ConsoleKey.RightArrow: MoveCursor(CursorDirection.RIGHT, inputBuffer.Length); break;
                    case ConsoleKey.Tab: writeBuffer = ProcessAutocompletion(); break;
                    default:
                        if (IsLegalInputChar(currentKey.KeyChar)) {
                            writeBuffer += currentKey.KeyChar; 
                        }
                        break;
                }

                inputBuffer.Append(writeBuffer);
                Console.Write(writeBuffer);
            }

            string userInput = inputBuffer.ToString().TrimStart();
            inputBuffer.Clear();

            return userInput;
        }

        string ProcessAutocompletion() {

            string autocompletionValue = "";

            if (autocompletionCache.Length > 0) {
                PrintAutocompletionCache(autocompletionCache);
                RedrawInput(inputBuffer);

                autocompletionCache = Array.Empty<string>();
                goto exit;
            }

            string[] matches = GetAutocompleteMatches(inputBuffer);

            if (matches.Length == 0) {
                ShellUtilities.PlayAlertBell();
            }
            else if (matches.Length == 1) {
                autocompletionValue = AutocompleteInput(matches[0], inputBuffer.Length);
            }
            else if (matches.Length > 1) {
                string commonPrefix = GetLongestCommonPrefix(matches);

                if (commonPrefix.Length > 0 && inputBuffer.ToString().Length < commonPrefix.Length) {
                    autocompletionValue = AutocompleteInput(commonPrefix, inputBuffer.Length, false);
                }
                else {
                    autocompletionCache = matches;
                    ShellUtilities.PlayAlertBell();
                }
            }

        exit:
            return autocompletionValue;
        }

        string[] GetAutocompleteMatches(StringBuilder inputBuffer) {

            List<string> matches = new();
            string input = inputBuffer.ToString();

            foreach (string completion in autocompletionMatches) {
                if (completion.StartsWith(input)) {
                    matches.Add(completion);
                }
            }

            matches.Sort();

            return matches.ToArray();
        }

        string AutocompleteInput(string completionMatch, int prefixLength, bool endCompletion = true) {

            string completion = "";

            for (int i = prefixLength; i < completionMatch.Length; ++i) {
                completion += completionMatch[i];
            }

            if (endCompletion) {
                completion += ShellConstants.SYMB_WHITESPACE;
            }
                 
            return completion;
        }

        void PrintAutocompletionCache(string[] autocompletionCache) {

            string cache = String.Join(ShellConstants.AUTOCOMPLETION_SEPARATOR, autocompletionCache);

            Console.Write(ShellConstants.SYMB_NEWLINE);
            Console.WriteLine(cache);
        }

        void RedrawInput(StringBuilder inputBuffer) {

            Console.Write(ShellConstants.NEW_PROMPT + inputBuffer.ToString());
        }

        void MoveCursor(CursorDirection direction, int inputLength) {

            (int Left, int Top) cursorPosition = Console.GetCursorPosition();
            int newPosition = cursorPosition.Left + (int)direction;

            if (newPosition > inputLength + ShellConstants.INPUT_BUFFER_START) {
                return;
            }

            if (newPosition < ShellConstants.INPUT_BUFFER_START) {
                return;
            }

            Console.SetCursorPosition(newPosition, cursorPosition.Top);
        }

        bool IsLegalInputChar(char character) {
            return Char.IsLetterOrDigit(character) || Char.IsWhiteSpace(character) || Char.IsSymbol(character) || Char.IsPunctuation(character);
        }

        static string GetLongestCommonPrefix(string[] matches) {

            string shortest = "";
            string commonPrefix = "";
            int shortestLen = Int32.MaxValue;

            foreach (string match in matches) {
                if (match.Length < shortestLen) {
                    shortest = match;
                    shortestLen = match.Length;
                }
            }

            for (int i = 0; i < shortest.Length; ++i) {
                for (int j = 0; j < matches.Length; ++j) {
                    if (shortest[i] != matches[j][i]) {
                        return commonPrefix;
                    }
                }
                commonPrefix += shortest[i];
            }

            return commonPrefix;
        }
    }
}