// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System;
using System.Text;

namespace CodeGen.Helpers
{
    /// <summary>
    /// Helper methods for consistently getting the same guid for a given string.
    /// </summary>
    public static class HashGuid
    {
        /// <summary>
        /// Returns a guid based on the SHA256 hash of the string,
        /// so identical strings will always give the same guid.
        /// </summary>
        /// <param name="src">A string.</param>
        /// <returns>A guid based on the hash of the string.</returns>
        public static Guid ToHashGuid(string src)
        {
            var bytes = Encoding.UTF8.GetBytes(src);

            var hashedBytes = System.Security.Cryptography.SHA256.HashData(bytes);

            Array.Resize(ref hashedBytes, 16);
            return new Guid(hashedBytes);
        }
    }
}
