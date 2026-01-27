namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// Main entry point for the Old Phone Pad library.
    /// </summary>
    public static class PhonePad
    {
        /// <summary>
        /// Converts a string of button presses into the corresponding text.
        /// </summary>
        /// <param name="input">The input string containing button presses.</param>
        /// <returns>The decoded message.</returns>
        public static string OldPhonePad(string input)
        {
            var processor = new PhonePadProcessor();
            return processor.Process(input);
        }
    }
}
