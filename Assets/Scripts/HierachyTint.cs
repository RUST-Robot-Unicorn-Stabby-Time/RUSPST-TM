using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class HierachyTint : MonoBehaviour
{
    [SerializeField] Color overlay;
    [SerializeField] Color sidebar;

#if UNITY_EDITOR
    static HierachyTint()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    public static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj)
        {
            HierachyTint tint = obj.GetComponent<HierachyTint>();

            if (tint)
            {
                Color color = tint.sidebar;

                Rect sidebarRect = new Rect(selectionRect.x + 60.0f - 28.0f - selectionRect.xMin, selectionRect.position.y, 2.0f, selectionRect.size.y);
                EditorGUI.DrawRect(sidebarRect, color);

                color = tint.overlay;
                color.a *= 0.2f;

                Rect backgroundRect = new Rect(selectionRect.x + 60.0f - 28.0f - selectionRect.xMin, selectionRect.position.y, selectionRect.size.x + selectionRect.xMin + 28.0f - 60.0f + 16.0f, selectionRect.size.y);
                EditorGUI.DrawRect(backgroundRect, color);
            }
        }
    }
#endif
}
