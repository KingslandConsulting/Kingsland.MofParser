using System.Globalization;

namespace Kingsland.MofParser.HtmlReport.Wrappers;

public static class HtmlHelper
{

    public static string HtmlEncode(string value)
    {
        return System.Web.HttpUtility.HtmlEncode(value);
    }

    public static string SplitTitleCaseWords(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        foreach (var upperChar in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            value = value.Replace(upperChar.ToString(CultureInfo.InvariantCulture), " " + upperChar);
        }
        return HtmlHelper.HtmlEncode(value);
    }

    public static string ReplaceUnderscores(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        value = value.Replace('_', ' ');
        return HtmlHelper.HtmlEncode(value);
    }

}
