#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using DefaultNamespace.FieldObjects;

[CustomEditor(typeof(FieldObjectTransparentView))]
public class FieldObjectTransparentViewEditor : Editor
{
    private static MethodInfo _setTransparentMaterialMI;
    private static MethodInfo _setDefaultMaterialMI;

    private void OnEnable()
    {
        var t = typeof(FieldObjectTransparentView);

        _setTransparentMaterialMI = t.GetMethod("SetTransparentMaterial",
            BindingFlags.Instance | BindingFlags.NonPublic);

        _setDefaultMaterialMI = t.GetMethod("SetDefaultMaterial",
            BindingFlags.Instance | BindingFlags.NonPublic);

        if (_setTransparentMaterialMI == null)
            Debug.LogError($"{t.Name}: method SetTransparentMaterial(Color) not found (private?).");
        if (_setDefaultMaterialMI == null)
            Debug.LogError($"{t.Name}: method SetDefaultMaterial() not found (private?).");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(12);
        EditorGUILayout.LabelField("Test Controls", EditorStyles.boldLabel);

        using (new EditorGUI.DisabledScope(target == null))
        {
            var view = (FieldObjectTransparentView)target;

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Apply Allowed (Green)"))
                    InvokeSetTransparent(view, Color.green);

                if (GUILayout.Button("Apply Prohibited (Red)"))
                    InvokeSetTransparent(view, Color.red);

                if (GUILayout.Button("Set Default"))
                    InvokeSetDefault(view);
            }

            EditorGUILayout.Space(6);

            // Доп. тест: кастомный цвет
            _customColor = EditorGUILayout.ColorField("Custom Color", _customColor);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Apply Custom Transparent"))
                    InvokeSetTransparent(view, _customColor);
            }

            EditorGUILayout.Space(6);

            // Важно: чтобы работало и в Edit Mode (без Play), принудительно перерисуем сцену
            if (GUILayout.Button("Repaint Scene View"))
                SceneView.RepaintAll();
        }
    }

    private Color _customColor = new Color(0f, 1f, 1f, 1f);

    private static void InvokeSetTransparent(FieldObjectTransparentView view, Color c)
    {
        if (_setTransparentMaterialMI == null) return;

        Undo.RecordObject(view, "Test Transparent Material");
        var renderer = GetRenderer(view);
        if (renderer != null) Undo.RecordObject(renderer, "Test Transparent Material");

        _setTransparentMaterialMI.Invoke(view, new object[] { c });

        MarkDirty(view, renderer);
    }

    private static void InvokeSetDefault(FieldObjectTransparentView view)
    {
        if (_setDefaultMaterialMI == null) return;

        Undo.RecordObject(view, "Test Default Material");
        var renderer = GetRenderer(view);
        if (renderer != null) Undo.RecordObject(renderer, "Test Default Material");

        _setDefaultMaterialMI.Invoke(view, null);

        MarkDirty(view, renderer);
    }

    // Пытаемся найти _renderer через SerializedObject (надёжнее, чем reflection полей напрямую)
    private static MeshRenderer GetRenderer(FieldObjectTransparentView view)
    {
        var so = new SerializedObject(view);
        var p = so.FindProperty("_renderer");
        return p != null ? p.objectReferenceValue as MeshRenderer : null;
    }

    private static void MarkDirty(UnityEngine.Object view, UnityEngine.Object renderer)
    {
        EditorUtility.SetDirty(view);
        if (renderer != null) EditorUtility.SetDirty(renderer);

        // Чтобы обновилось в Edit Mode
        SceneView.RepaintAll();
    }
}
#endif
