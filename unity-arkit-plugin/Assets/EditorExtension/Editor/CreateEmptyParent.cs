using UnityEditor;
using UnityEngine;
using System.Linq;

public static class CreateEmptyParent
{

    [MenuItem("GameObject/Group Selected %g")]

    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms.OrderBy(t => t.GetSiblingIndex()))
        {
            Undo.SetTransformParent(transform, go.transform, "Group Selected");
        }
        Selection.activeGameObject = go;
    }
}