using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace InfoCards.BLL.API.Controllers
{
    internal static class InfoCardControllerHelpers
    {

        [NonAction]
        private static bool IsBase64String(this string str)
        {
            return (str.Length % 4 == 0) && Regex.IsMatch(str, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);
        }
    }
}