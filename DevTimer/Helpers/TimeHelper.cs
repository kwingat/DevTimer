using System;
using System.Globalization;

namespace DevTimer.Helpers
{
    public static class TimeHelper
    {
        public static DateTime ToClientTime(this DateTime dt)
        {
            var timeOffSet = System.Web.HttpContext.Current.Session["timezoneoffset"];

            if (timeOffSet != null)
            {
                var offset = Int32.Parse(timeOffSet.ToString());
                dt = dt.AddMinutes(-1 * offset);

                return Convert.ToDateTime(dt.ToString(CultureInfo.InvariantCulture));
            }

            return Convert.ToDateTime(dt.ToLocalTime().ToString(CultureInfo.InvariantCulture));
        }
    }
}