using System.Globalization;
using System.Text.RegularExpressions;

namespace DenKhangThinh.Helpers;

public static class Utils
{
    public static string? ToVnd(this decimal? value)
    {
        if (value != null)
        {
            var value2 = value ?? 0;
            var nfi = new NumberFormatInfo()
            {
                NumberDecimalDigits = 0,
                NumberGroupSeparator = "."
            };
            return $"{value2.ToString("N", nfi)}đ";
        }
        return string.Empty;
    }
    public static IEnumerable<List<T>> Partition<T>(this IList<T> source, Int32 size)
    {
        for (int i = 0; i < Math.Ceiling(source.Count / (Double)size); i++)
            yield return new List<T>(source.Skip(size * i).Take(size));
    }
    public static bool IsMobile(string userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return false;
        //tablet
        if (Regex.IsMatch(userAgent, "(tablet|ipad|playbook|silk)|(android(?!.*mobile))", RegexOptions.IgnoreCase))
            return true;
        //mobile
        const string mobileRegex =
            "blackberry|iphone|mobile|windows ce|opera mini|htc|sony|palm|symbianos|ipad|ipod|blackberry|bada|kindle|symbian|sonyericsson|android|samsung|nokia|wap|motor";

        if (Regex.IsMatch(userAgent, mobileRegex, RegexOptions.IgnoreCase)) return true;
        //not mobile 
        return false;
    }
}
