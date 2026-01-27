using System;
using System.Text;

namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// internal processor that handles the state machine logic for the phone pad.
    /// </summary>
    internal class PhonePadProcessor
    {
        private readonly StringBuilder _resultBuffer = new StringBuilder();
        private char? _currentButton = null;
        private int _pressCount = 0;

        /// <summary>
        /// Processes the input string and returns the decoded message.
        /// </summary>
        /// <param name="input">The sequence of button presses.</param>
        /// <returns>The decoded string.</returns>
        public string Process(string input)
        {
            if (string.IsNullOrEmpty(input) || !input.EndsWith("#"))
            {
                throw new ArgumentException("Error: Input must end with a send button '#'.");
            }

            foreach (char c in input)
            {
                if (c == '#')
                {
                    Commit();
                    return _resultBuffer.ToString();
                }
                else if (c == '*')
                {
                    Commit(); // Confirm pending char first
                    _currentButton = null;
                    if (_resultBuffer.Length > 0)
                    {
                        _resultBuffer.Length--; // Backspace
                    }
                }
                else if (c == ' ')
                {
                    Commit();
                    _currentButton = null;
                }
                else if (StandardKeypadLayout.Mapping.ContainsKey(c))
                {
                    if (_currentButton == c)
                    {
                        _pressCount++;
                    }
                    else
                    {
                        Commit();
                        _currentButton = c;
                        _pressCount = 1;
                    }
                }
                // Ignore invalid characters as per implicit requirement to process valid input loop, 
                // or we could throw. The requirements didn't specify handling invalid chars explicitly 
                // other than the structure, but usually skipped or cycle logic applies. 
                // We will stick to processing known digits/logic.
            }

            // Should not be reached due to validation check at start and return on '#'
            return _resultBuffer.ToString();
        }

        private void Commit()
        {
            if (_currentButton == null)
            {
                return;
            }

            if (StandardKeypadLayout.Mapping.TryGetValue(_currentButton.Value, out string options))
            {
                int index = (_pressCount - 1) % options.Length;
                _resultBuffer.Append(options[index]);
            }

            _currentButton = null;
            _pressCount = 0;
        }
    }
}
