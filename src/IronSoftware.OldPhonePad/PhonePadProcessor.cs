using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// Internal processor that handles the state machine logic for the phone pad.
    /// Uses modern C# patterns including aggressive inlining, span-based processing, and immutable state.
    /// </summary>
    internal sealed class PhonePadProcessor
    {
        private const string InvalidInputMessage = "Error: Input must end with a send button '#'.";
        private const char SendButton = '#';
        private const char BackspaceButton = '*';
        private const char PauseButton = ' ';
        private const int DefaultCapacity = 32; // Pre-allocated capacity for StringBuilder

        private readonly IKeypadLayout _keypadLayout;
        private readonly StringBuilder _resultBuffer;
        private ButtonState _currentState;

        /// <summary>
        /// Represents the state of the current button being pressed.
        /// Implemented as a readonly record struct for immutability.
        /// </summary>
        private readonly record struct ButtonState
        {
            public char? Button { get; init; }
            public int PressCount { get; init; }

            public bool IsActive => Button.HasValue;

            public static ButtonState Empty => default;

            public ButtonState IncrementPress() => this with { PressCount = PressCount + 1 };

            public static ButtonState Start(char button) => new() { Button = button, PressCount = 1 };
        }

        /// <summary>
        /// Initializes a new instance of the PhonePadProcessor with the specified keypad layout.
        /// </summary>
        /// <param name="keypadLayout">The keypad layout to use for decoding.</param>
        /// <exception cref="ArgumentNullException">Thrown when keypadLayout is null.</exception>
        public PhonePadProcessor(IKeypadLayout keypadLayout)
        {
            _keypadLayout = keypadLayout ?? throw new ArgumentNullException(nameof(keypadLayout));
            _resultBuffer = new StringBuilder(DefaultCapacity);
            _currentState = ButtonState.Empty;
        }

        /// <summary>
        /// Processes the input string and returns the decoded message.
        /// </summary>
        /// <param name="input">The sequence of button presses.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="ArgumentException">Thrown when input is invalid.</exception>
        public string Process(string input)
        {
            ValidateInput(input);
            return ProcessCharacters(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateInput(string? input)
        {
            if (string.IsNullOrEmpty(input) || !input.EndsWith(SendButton))
            {
                throw new ArgumentException(InvalidInputMessage, nameof(input));
            }
        }

        private string ProcessCharacters(string input)
        {
            // Use ReadOnlySpan for better performance (no allocations)
            ReadOnlySpan<char> chars = input.AsSpan();
            
            foreach (char c in chars)
            {
                ProcessCharacter(c);

                if (c == SendButton)
                {
                    return _resultBuffer.ToString();
                }
            }

            return _resultBuffer.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessCharacter(char c)
        {
            switch (c)
            {
                case SendButton:
                    HandleSend();
                    break;
                case BackspaceButton:
                    HandleBackspace();
                    break;
                case PauseButton:
                    HandlePause();
                    break;
                default:
                    HandleDigit(c);
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleSend()
        {
            CommitCurrentButton();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleBackspace()
        {
            CommitCurrentButton();
            _currentState = ButtonState.Empty;

            if (_resultBuffer.Length > 0)
            {
                _resultBuffer.Length--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandlePause()
        {
            CommitCurrentButton();
            _currentState = ButtonState.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleDigit(char digit)
        {
            if (!_keypadLayout.Mapping.ContainsKey(digit))
            {
                return; // Ignore invalid characters
            }

            if (_currentState.Button == digit)
            {
                _currentState = _currentState.IncrementPress();
            }
            else
            {
                CommitCurrentButton();
                _currentState = ButtonState.Start(digit);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CommitCurrentButton()
        {
            if (!_currentState.IsActive)
            {
                return;
            }

            if (_keypadLayout.Mapping.TryGetValue(_currentState.Button!.Value, out string? options))
            {
                int index = (_currentState.PressCount - 1) % options.Length;
                _resultBuffer.Append(options[index]);
            }

            _currentState = ButtonState.Empty;
        }
    }
}
