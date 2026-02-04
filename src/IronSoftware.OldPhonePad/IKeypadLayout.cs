using System.Collections.Generic;

namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// Defines the contract for keypad layouts used in phone pad decoding.
    /// </summary>
    public interface IKeypadLayout
    {
        /// <summary>
        /// Gets the mapping between button characters and their corresponding character sequences.
        /// </summary>
        /// <remarks>
        /// The key represents the button character (e.g., '2', '3', '4').
        /// The value represents the sequence of characters that can be typed by pressing that button repeatedly.
        /// </remarks>
        IReadOnlyDictionary<char, string> Mapping { get; }
    }
}