using System.Collections.Generic;

namespace IronSoftware.OldPhonePad
{
    /// <summary>
    /// Provides the standard keypad mapping for the Old Phone Pad.
    /// </summary>
    public static class StandardKeypadLayout
    {
        public static readonly Dictionary<char, string> Mapping = new Dictionary<char, string>
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
            { '0', " 0" } // Space then 0
        };
    }
}
