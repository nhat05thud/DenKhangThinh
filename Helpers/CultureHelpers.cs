namespace DenKhangThinh.Helpers;

public static class CultureHelpers
{
    public static string GetCultureNameFromCulturecode(string code)
    {
        switch (code.ToLower())
        {
            case "vi-vn":
                return "VI";
            case "en-us":
                return "EN";
            default:
                return "VI";
        }
    }
}
