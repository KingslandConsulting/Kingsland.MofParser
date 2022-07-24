namespace Kingsland.MofParser.UnitTests.Extensions;

internal static class StringExtensions
{


    /// <summary>
    /// Similar to the Kotlin trimIndent function.
    /// Removes a common leading indent from the input text
    /// </summary>
    /// <param name="source"></param>
    /// <param name="newline"></param>
    /// <returns></returns>
    /// <remarks>
    /// See https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.text/trim-indent.html
    /// </remarks>
    public static string TrimIndent(this string source, string newline)
    {
        static int CountLeadingWhiteSpace(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if (!char.IsWhiteSpace(str[i]))
                {
                    return i;
                }
            }
            // there are no non-whitespace characters in the string.
            // this includes where a string contains *only* whitespace.
            return -1;
        }
        // based on:
        //   + https://stackoverflow.com/a/36572281/3156906
        //   + https://stackoverflow.com/a/47459643/3156906
        //   + https://stackoverflow.com/a/20411839/3156906
        var lines = source
            .Split(newline)
            .Select(
                str => (String: str, LeadingWhiteSpace: CountLeadingWhiteSpace(str))
            );
        var indent = lines
            .Where(str => str.LeadingWhiteSpace > -1)
            .Min(str => str.LeadingWhiteSpace);
        return string.Join(
            newline,
            lines.Select(
                str => str.String.Substring(Math.Min(str.String.Length, indent)))
            );
    }

    public static string TrimString(this string source, string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            throw new ArgumentException(null, nameof(str));
        }
        if (string.IsNullOrEmpty(source))
        {
            return source;
        }
        // skip over any leading str instances
        var startIndex = 0;
        while (
            (startIndex + str.Length <= source.Length) &&
            (source.Substring(startIndex, str.Length) == str)
        )
        {
            startIndex += str.Length;
        }
        // skip over any trailing str instances
        var endIndex = source.Length - 1;
        while (
            (endIndex - str.Length >= startIndex) &&
            (source.Substring(endIndex - str.Length + 1, str.Length) == str)
        )
        {
            endIndex -= str.Length;
        }
        // remove the leading and trailing str
        return source.Substring(startIndex, endIndex - startIndex + 1);
    }

}
