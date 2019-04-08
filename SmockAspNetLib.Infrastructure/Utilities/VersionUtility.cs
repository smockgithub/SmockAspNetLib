using System;
using System.IO;
using System.Reflection;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    public class VersionUtility
    {
        private static string releaseTicks;
        private static DateTime releaseDate;
        public const string Version = "4.5.0.0";

        public static string ReleaseTicks
        {
            get
            {
                if (string.IsNullOrEmpty(releaseTicks))
                {
                    releaseTicks = ReleaseDate.Ticks.ToString();
                }
                return releaseTicks;
            }
        }

        public static DateTime ReleaseDate
        {
            get
            {
                if (releaseDate == null || releaseDate.Year < DateTime.Now.Year)
                {
                    releaseDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                }
                return releaseDate;
            }
        }
    }
}
