namespace ExtentoinMethods;

public static  class Extentions
{
    public static string NullIfEmpty(this string str)
    {
        return string.IsNullOrEmpty(str) ? null : str;
    }
}