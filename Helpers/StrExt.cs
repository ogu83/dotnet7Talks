using System.Web;

namespace dotnet7Talks.Helpers;

public static class StrExt
{
    public static string ToHtml(this string text)
    {
        ReadOnlySpan<byte> AuthWithTrailingSpace = new byte[] { 0x41, 0x55, 0x54, 0x48, 0x20 };
        ReadOnlySpan<byte> AuthStringLiteral = "AUTH "u8; //C# 11
        byte[] AuthStringLiteralBytes = AuthStringLiteral.ToArray();

        text = HttpUtility.HtmlEncode(text);
        text = text.Replace("\r\n", "\r");
        text = text.Replace("\n", "\r");
        text = text.Replace("\r", "<br>\r\n");
        text = text.Replace("  ", " &nbsp;");
        return text;
    }
}