using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime;

public static class NotesDatabaseUtility
{
    private const string DatabaseFolder = "Assets/Notes";
    private const string DatabaseName = "NotesDatabase.asset";

    public static NotesDatabase GetOrCreateDatabase()
    {
        // Try to find existing one
        string[] guids = AssetDatabase.FindAssets("t:NotesDatabase");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<NotesDatabase>(path);
        }

        // Create if missing
        if (!AssetDatabase.IsValidFolder(DatabaseFolder))
            AssetDatabase.CreateFolder("Assets", "Notes");

        var newDb = ScriptableObject.CreateInstance<NotesDatabase>();
        AssetDatabase.CreateAsset(newDb, Path.Combine(DatabaseFolder, DatabaseName));
        AssetDatabase.SaveAssets();

        Debug.Log("🗃 Created new NotesDatabase at " + Path.Combine(DatabaseFolder, DatabaseName));
        return newDb;
    }

    public static void AddNote(NotesSO note)
    {
        var db = GetOrCreateDatabase();
        db.notes.Add(note);
        EditorUtility.SetDirty(db);
        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Added '{note.title}' to NotesDatabase");
        
    }
    
    
}