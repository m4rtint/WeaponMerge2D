using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace _WeaponMerge.Tools
{
    public static class PanicHelper
    {
        public static void CheckAndPanicIfNullOrEmpty<T>(T[] array,
    [CallerMemberName] string memberName = "",
    [CallerFilePath] string filePath = "") where T : UnityEngine.Object
        {
            if (array == null || array.Length == 0)
            {
                var className = System.IO.Path.GetFileNameWithoutExtension(filePath);
                Panic(new Exception($"{className}.{memberName}: {typeof(T).Name} array is null or empty"));
            }
        }
        
        // Helper method to check for null and panic if null
        public static void CheckAndPanicIfNull<T>(T obj,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "") where T : UnityEngine.Object
        {
            if (obj == null)
            {
                var className = System.IO.Path.GetFileNameWithoutExtension(filePath);
                Panic(new Exception($"{className}.{memberName}: {typeof(T).Name} is not set"));
            }
        }
        
        // General panic method that handles any exception
        public static void Panic(Exception exception)
        {
            // Break into the debugger
            System.Diagnostics.Debugger.Break();

            // Print the exception
            Debug.LogException(exception);
            // Quit the app
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(1);
#endif
        }
    }
}
