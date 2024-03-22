// ----------------------------------------------------------------------------
// <copyright file="DateTimeScrubber.cs" company="L0gg3r">
// Copyright (c) L0gg3r Project
// </copyright>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;

namespace WB.TestUtils.TextScrubbers
{
    /// <summary>
    /// Provides extension methods to scrub date and time values from strings.
    /// </summary>
    public static class DateTimeScrubber
    {
        // ┌────────────────────────────────────────────────────────────────────────────────┐
        // │ Public Methods                                                                 │
        // └────────────────────────────────────────────────────────────────────────────────┘

        /// <summary>
        /// Scrubs a <see cref="DateTime"/> value from <paramref name="this"/> string.
        /// </summary>
        /// <remarks>
        /// All findings will be replaced with a string of equal length. If the <paramref name="replacement"/> is <c>null</c>
        /// (default), the findings will be replaced with a string of spaces.
        /// </remarks>
        /// <param name="this">The extended string.</param>
        /// <param name="format">The format string that is for searching date time values.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>The scrubbed string.</returns>
        public static string ScrubDateTime(this string @this, string format, string? replacement = null)
        {
            ArgumentNullException.ThrowIfNull(@this);

            ArgumentNullException.ThrowIfNull(format);

            string result = @this;

            foreach (string finding in GetFindings(@this, format))
            {
                string paddedReplacement = ReplacementScrubber.CreateReplacement(replacement, finding.Length);

                result = result.Replace(finding, paddedReplacement, StringComparison.InvariantCulture);
            }

            return result;
        }

        // ┌────────────────────────────────────────────────────────────────────────────────┐
        // │ Private Methods                                                                │
        // └────────────────────────────────────────────────────────────────────────────────┘
        private static IEnumerable<string> GetFindings(string input, string format)
        {
            string cleanedFormat = format.Replace(@"\", string.Empty, StringComparison.InvariantCulture);

            for (int i = 0; i <= input.Length - cleanedFormat.Length; i++)
            {
                string subString = input.Substring(i, cleanedFormat.Length);

                if (DateTime.TryParseExact(subString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    yield return subString;
                }
            }
        }
    }
}
