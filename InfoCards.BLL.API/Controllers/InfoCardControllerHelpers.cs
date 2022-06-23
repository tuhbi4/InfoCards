using System.Text.RegularExpressions;

namespace InfoCards.BLL.API.Controllers
{
    public static class InfoCardControllerHelpers
    {
        public static bool IsBase64String(this string str)
        {
            return (str.Length % 4 == 0) && Regex.IsMatch(str, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);
        }
    }
}