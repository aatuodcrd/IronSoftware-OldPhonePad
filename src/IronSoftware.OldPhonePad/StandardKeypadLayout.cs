using System.Collections.Frozen;
using System.Collections.Generic;

namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// Provides the standard keypad mapping for the Old Phone Pad.
    /// Uses FrozenDictionary for read-only, immutable access.
    /// </summary>
    public sealed class StandardKeypadLayout : IKeypadLayout
    {
        /// <summary>
        /// Gets the singleton instance of the standard keypad layout.
        /// </summary>
        public static readonly StandardKeypadLayout Instance = new();

        /// <summary>
        /// Gets the immutable mapping between button characters and their corresponding character sequences.
        /// </summary>
        /// <remarks>
        /// Uses FrozenDictionary, which is optimized for read-only dictionary operations.
        /// </remarks>
        public IReadOnlyDictionary<char, string> Mapping { get; }

        private StandardKeypadLayout()
        {
            Mapping = new Dictionary<char, string>
            {
                { '1', "1" },
                { '2', "ABC2" },
                { '3', "DEF3" },
                { '4', "GHI4" },
                { '5', "JKL5" },
                { '6', "MNO6" },
                { '7', "PQRS7" },
                { '8', "TUV8" },
                { '9', "WXYZ9" },
                { '0', " 0" }
            }.ToFrozenDictionary();
        }
    }
}

