using System.Web.Http;
using DevTimer.Filters;

namespace DevTimer
{
    public static class GlobalConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidationHttpFilter());
        }
    }
}