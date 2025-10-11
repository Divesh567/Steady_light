using Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime;
using UnityEditor;

namespace Immersiveorama.EditorTools.Immersiveorama.Notes.Editor
{
    [CustomEditor(typeof(NotesSO))]
    public class NotesSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            NotesSO note = (NotesSO)target;

            EditorGUI.BeginChangeCheck();

            // === Default Inspector for fields ===
            DrawDefaultInspector();

            if (EditorGUI.EndChangeCheck())
            {
                note.MarkModified();
                EditorUtility.SetDirty(note);
                AssetDatabase.SaveAssets();
            }

            // === Show metadata (read-only) ===
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Created", note.DateCreated);
            EditorGUILayout.LabelField("Last Modified", note.DateModified);
        }
    }
}
