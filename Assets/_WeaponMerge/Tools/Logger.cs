using System;
using System.Collections.Generic;
using UnityEngine;

namespace _WeaponMerge.Tools
{
    // Add in ENUM and Dictionary
    public enum LogKey
    {
        State
    }

    public enum LogColor
    {
        None,
        Black,
        Blue,
        Red,
        Yellow,
        Green
    }
    
    public static class Logger
    {
        // Add in ENUM and Dictionary
        private static Dictionary<LogKey, bool> _configuration = new()
        {
            {
                LogKey.State, false
            }
        };

        public static void Configure(Dictionary<LogKey, bool> setup)
        {
            _configuration = setup;
        }
        
        public static void Log(string message, LogKey key, GameObject gameObject = null, LogColor color = LogColor.Black)
        {
            if (_configuration.TryGetValue(key, out var shouldLog) && shouldLog)
            {
                string gameObjectData()
                {
                    if (gameObject)
                    {
                        return $"[{gameObject.name}_{gameObject.GetInstanceID()}]";
                    }

                    return "";
                }
                
                if (color != LogColor.None)
                {
                    Debug.Log($"<color={MapToColor(color)}>{gameObjectData()}{message}</color>");
                }
                else
                {
                    Debug.Log(message);
                }
            }
        }

        private static String MapToColor(LogColor color)
        {
            switch (color)
            {
                case LogColor.None:
                    return null;
                case LogColor.Black:
                    return "black";
                case LogColor.Blue:
                    return "cyan";
                case LogColor.Red:
                    return "red";
                case LogColor.Yellow:
                    return "yellow";
                case LogColor.Green:
                    return "green";
            }

            return null;
        }
    }
}