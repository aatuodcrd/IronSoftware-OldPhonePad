namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// Main entry point for the Old Phone Pad library.
    /// Provides both static convenience methods and extensible instance-based API.
    /// </summary>
    public static class PhonePad
    {
        /// <summary>
        /// Converts a string of button presses into the corresponding text using the standard keypad layout.
        /// </summary>
        /// <param name="input">The input string containing button presses (must end with '#').</param>
        /// <returns>The decoded message.</returns>
        /// <exception cref="System.ArgumentException">Thrown when input is null, empty, or doesn't end with '#'.</exception>
        /// <example>
        /// <code>
        /// string result = PhonePad.Decode("4433555 555666#");
        /// // result = "HELLO"
        /// </code>
        /// </example>
        public static string Decode(string input)
        {
            return Decode(input, StandardKeypadLayout.Instance);
        }

        /// <summary>
        /// Converts a string of button presses into the corresponding text using a custom keypad layout.
        /// </summary>
        /// <param name="input">The input string containing button presses.</param>
        /// <param name="keypadLayout">The keypad layout to use for decoding.</param>
        /// <returns>The decoded message.</returns>
        /// <exception cref="System.ArgumentException">Thrown when input is invalid.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when keypadLayout is null.</exception>
        public static string Decode(string input, IKeypadLayout keypadLayout)
        {
            var processor = new PhonePadProcessor(keypadLayout);
            return processor.Process(input);
        }

        /// <summary>
        /// Legacy method name for backward compatibility.
        /// </summary>
        /// <param name="input">The input string containing button presses.</param>
        /// <returns>The decoded message.</returns>
        [Obsolete("Use Decode() instead.")]
        public static string OldPhonePad(string input) => Decode(input);
    }
}
