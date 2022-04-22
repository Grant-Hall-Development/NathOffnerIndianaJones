using System.Collections;
using UnityEngine;

namespace Base.Utility
{

    public static class Pr
    {

        public enum LogMode
        {
            Disabled,
            Enabled
        }
        public static LogMode mode = LogMode.Enabled;

        public static void Error(string error)
        {
            if (mode == LogMode.Disabled) return;
            Debug.LogError(error);
        }

        public static void Warning(string warning)
        {
            if (mode == LogMode.Disabled) return;
            Debug.LogWarning(warning);
        }

        public static void Log(string log)
        {
            if (mode == LogMode.Disabled) return;
            Debug.Log(log);
        }
    }

}