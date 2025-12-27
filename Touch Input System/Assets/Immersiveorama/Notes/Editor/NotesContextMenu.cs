using UnityEditor;
using UnityEngine;
using System.IO;
using Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime;

public static class NotesContextMenu
{
    [MenuItem("GameObject/Add Note", false, 49)]
    private static void AddNoteToSelectedObject(MenuCommand command)
    {
        GameObject go = Selection.activeGameObject;
        if (go == null) return;
        CreateNoteForObject(go);
    }

    [MenuItem("CONTEXT/Transform/Add Note")]
    private static void AddNoteToContext(MenuCommand command)
    {
        GameObject go = ((Transform)command.context).gameObject;
        CreateNoteForObject(go);
    }

    private static void CreateNoteForObject(GameObject go)
    {
        var note = ScriptableObject.CreateInstance<NotesSO>();
        note.title = $"Note: {go.name}";
        note.description = "";
        note.attachedScenePath = go.scene.path;
        note.attachedHierarchyPath = GetHierarchyPath(go);

        // Save as asset
        string folder = "Assets/Notes";
        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets", "Notes");

        string noteAssetPath = AssetDatabase.GenerateUniqueAssetPath($"{folder}/{go.name}_Note.asset");
        AssetDatabase.CreateAsset(note, noteAssetPath);
        AssetDatabase.SaveAssets();

        NotesDatabaseUtility.AddNote(note);
    }

    private static string GetHierarchyPath(GameObject go)
    {
        if (go == null) return string.Empty;
        string path = go.name;
        Transform t = go.transform;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}