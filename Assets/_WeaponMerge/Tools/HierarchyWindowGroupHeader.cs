using UnityEngine;
using UnityEditor;

namespace Tools
{
    /// <summary>
    /// Hierarchy Window Group Header
    /// </summary>
    #if UNITY_EDITOR
        [InitializeOnLoad]
        public static class HierarchyWindowGroupHeader
        {
            static HierarchyWindowGroupHeader()
            {
                EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
            }

            private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
            {
                var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                if (gameObject != null && gameObject.name.StartsWith("---", System.StringComparison.Ordinal))
                {
                    EditorGUI.DrawRect(selectionRect, Color.gray);
                    EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("-", string.Empty).ToUpperInvariant());
                }
            }
        }
    #endif
}