using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookingApp.WPF.View.Guest.GuestTools
{
    public class PathConverter
    {
        public string ConvertToRelativePath(string inputPath)
        {

            string pattern = @"\\";


            string replacedPath = Regex.Replace(inputPath, pattern, "/");


            if (replacedPath.StartsWith("Resources/Images/"))
            {
                replacedPath = "../../../" + replacedPath;
            }

            return replacedPath;
        }
    }
}
