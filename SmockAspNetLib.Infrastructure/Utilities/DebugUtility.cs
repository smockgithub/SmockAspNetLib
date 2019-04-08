namespace SmockAspNetLib.Infrastructure.Utilities
{
    public static class DebugUtility
    {
        public static bool IsDebug
        {
            get
            {
#pragma warning disable
#if DEBUG
                return true;
#endif
                return false;
#pragma warning restore
            }
        }
    }
}
