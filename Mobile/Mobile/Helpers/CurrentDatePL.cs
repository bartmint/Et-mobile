using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mobile.Helpers
{
    public static class CurrentDatePL
    {
        public static string GetCurrentDate()
        {
            var day = DateTime.Now.ToString("dddd", CultureInfo.CreateSpecificCulture("pl-PL"));
            var month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("pl-PL"));
            var year = DateTime.Now.ToString("yyyy", CultureInfo.CreateSpecificCulture("pl-PL"));

            return $"{FirstLetterToUpper(day)}, {FirstLetterToUpper(month)} {year} roku";
        }
        public static string FirstLetterToUpper(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);

        }
    }
}
