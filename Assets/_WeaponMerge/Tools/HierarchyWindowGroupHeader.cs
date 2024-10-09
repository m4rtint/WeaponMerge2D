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
                    // Draw a background rectangle (optional)
                    EditorGUI.DrawRect(selectionRect, Color.gray);

                    // Get the label without dashes and make it uppercase
                    string label = gameObject.name.Replace("-", string.Empty).ToUpperInvariant();

                    // Get the style for the label
                    GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
                    style.alignment = TextAnchor.MiddleCenter; // Center the text
                    style.normal.textColor = Color.white;      // Set the text color to black


                    // Adjust the label size to fit inside the rect
                    Vector2 labelSize = style.CalcSize(new GUIContent(label));

                    // Calculate centered position
                    Rect centeredRect = new Rect(
                        selectionRect.x + (selectionRect.width - labelSize.x) / 2,  // X Position
                        selectionRect.y + (selectionRect.height - labelSize.y) / 2, // Y Position
                        labelSize.x, 
                        labelSize.y
                    );

                    // Draw the centered label
                    EditorGUI.LabelField(centeredRect, label, style);

                }
            }
        }
    #endif
}