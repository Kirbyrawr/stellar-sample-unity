using System.Collections;
using System.Collections.Generic;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.requests;

namespace UStellar.Core
{
    public enum DebugType { Info, Warning, Error }

    public class UStellarDebug
    {
        private static bool debugEnabled = true;

        public static void Debug(string message, DebugType type = DebugType.Info, bool addNewLineAtEnd = true)
        {
            if (!debugEnabled) { return; }

            if(addNewLineAtEnd) 
            {
               message = string.Concat(message, System.Environment.NewLine);
            }

            switch (type)
            {
                case DebugType.Info:
                    UnityEngine.Debug.Log(message);
                    break;

                case DebugType.Warning:
                    UnityEngine.Debug.LogWarning(message);
                    break;

                case DebugType.Error:
                    UnityEngine.Debug.LogError(message);
                    break;
            }
        }

        public static void SetDebug(bool enabled)
        {
            debugEnabled = enabled;
        }
    }
}
