// ----------------------------------------------------------------------------
// <copyright file="ReplacementScrubber.cs" company="WB">
// Copyright (c) WB Project
// </copyright>
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WB.TestUtils.TextScrubbers
{
    /// <summary>
    /// Provides extension methods to scrub strings by replacing findings with a replacement string of equal length.
    /// </summary>
    public static class ReplacementScrubber
    {
        // ┌────────────────────────────────────────────────────────────────────────────────┐
        // │ Public Methods                                                                 │
        // └────────────────────────────────────────────────────────────────────────────────┘

        /// <summary>
        /// Scrubs all findings of <paramref name="pattern"/> from <paramref name="this"/> string.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="pattern">The pattern to replace.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, string pattern)
        {
            string groupName = "replace";

            return @this.Scrub(new Regex($"(?'{groupName}'({Regex.Escape(pattern)}))"), null, groupName, true);
        }

        /// <summary>
        /// Scrubs all findings of <paramref name="pattern"/> from <paramref name="this"/> string.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="pattern">The pattern to replace.</param>
        /// <param name="replaceWithEqualLength">If set to <c>true</c> all findings are replaced with a string of equal length.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, string pattern, bool replaceWithEqualLength)
        {
            string groupName = "replace";

            return @this.Scrub(new Regex($"(?'{groupName}'({Regex.Escape(pattern)}))"), null, groupName, replaceWithEqualLength);
        }

        /// <summary>
        /// Scrubs all findings matching <paramref name="regex"/> from <paramref name="this"/> string.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="regex">The <see cref="Regex"/> used for searching.</param>
        /// <param name="groupName">The match group name of <paramref name="regex"/>.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, Regex regex, string groupName)
        {
            return @this.Scrub(regex, null, groupName, true);
        }

        /// <summary>
        /// Scrubs all findings matching <paramref name="regex"/> from <paramref name="this"/> string.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="regex">The <see cref="Regex"/> used for searching.</param>
        /// <param name="groupName">The match group name of <paramref name="regex"/>.</param>
        /// <param name="replaceWithEqualLength">If set to <c>true</c> all findings are replaced with a string of equal length.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, Regex regex, string groupName, bool replaceWithEqualLength)
        {
            return @this.Scrub(regex, null, groupName, replaceWithEqualLength);
        }

        /// <summary>
        /// Scrubs all findings of <paramref name="pattern"/> from <paramref name="this"/> string by replacing it with <paramref name="replacement"/>.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="pattern">The pattern to replace.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, string pattern, string? replacement)
        {
            string groupName = "replace";

            return @this.Scrub(new Regex($"(?'{groupName}'({Regex.Escape(pattern)}))"), replacement, groupName, true);
        }

        /// <summary>
        /// Scrubs all findings of <paramref name="pattern"/> from <paramref name="this"/> string by replacing it with <paramref name="replacement"/>.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="pattern">The pattern to replace.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="replaceWithEqualLength">If set to <c>true</c> all findings are replaced with a string of equal length.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, string pattern, string? replacement, bool replaceWithEqualLength)
        {
            string groupName = "replace";

            return @this.Scrub(new Regex($"(?'{groupName}'({Regex.Escape(pattern)}))"), replacement, groupName, replaceWithEqualLength);
        }

        /// <summary>
        /// Scrubs all findings matching <paramref name="regex"/> from <paramref name="this"/> string by replacing it with <paramref name="replacement"/>.
        /// </summary>
        /// <param name="this">The extended string.</param>
        /// <param name="regex">The <see cref="Regex"/> used for searching.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="groupName">The match group name of <paramref name="regex"/>.</param>
        /// <param name="replaceWithEqualLength">If set to <c>true</c> all findings are replaced with a string of equal length.</param>
        /// <returns>The scrubbed string.</returns>
        public static string Scrub(this string @this, Regex regex, string? replacement, string groupName, bool replaceWithEqualLength = true)
        {
            System.ArgumentNullException.ThrowIfNull(@this);
            System.ArgumentNullException.ThrowIfNull(regex);

            MatchCollection matchCollection = regex.Matches(@this);

            IEnumerable<string> findings = matchCollection.Cast<Match>().Select(mc => mc.Groups[groupName].Value).Distinct().OrderByDescending(s => s.Length);

            string result = @this;

            foreach (string finding in findings)
            {
                int? length = GetValueOrNull(finding.Length, replaceWithEqualLength);

                string replacement_ = CreateReplacement(replacement, length);

                result = result.Replace(finding, replacement_, System.StringComparison.InvariantCulture);
            }

            return result;
        }

        // ┌────────────────────────────────────────────────────────────────────────────────┐
        // │ Internal Methods                                                               │
        // └────────────────────────────────────────────────────────────────────────────────┘

        /// <summary>
        /// Creates a replacement by enclosing <paramref name="replacement"/> with curly braces.
        /// </summary>
        /// <param name="replacement">The string that shall be used as replacement.</param>
        /// <param name="length">The total length of the replacement.</param>
        /// <returns>The replacement string.</returns>
        internal static string CreateReplacement(string? replacement, int? length = null)
        {
            if (length != null && length.Value < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(length), "The length must be greater than or equal to zero.");
            }

            if (replacement is null)
            {
                if (length is null)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Empty.PadRight(length.Value, ' ');
                }
            }
            else
            {
                if (length is null)
                {
                    return $"{{{replacement}}}";
                }
                else
                {
                    switch (length)
                    {
                        case 0: return string.Empty;
                        case 1: return "{";
                        case 2: return "{}";
                        default:
                            if (replacement.Length + 2 > length)
                            {
                                return $"{{{replacement.Substring(0, length.Value - 2)}}}";
                            }
                            else if (replacement.Length == length)
                            {
                                return $"{{{replacement}}}";
                            }
                            else
                            {
                                return $"{{{replacement}{string.Empty.PadRight(length.Value - replacement.Length - 2, '-')}}}";
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the <paramref name="value"/> or <c>null</c> if <paramref name="returnValue"/> is <c>false</c>.
        /// </summary>
        /// <param name="value">The length.</param>
        /// <param name="returnValue">A flag indicating whether to return the <paramref name="value"/> (<c>true</c>) or <c>null</c> (<c>false</c>).</param>
        /// <returns>The length or <c>null</c>.</returns>
        internal static int? GetValueOrNull(int value, bool returnValue)
        {
            if (returnValue)
            {
                return value;
            }
            else
            {
                return null;
            }
        }
    }
}
