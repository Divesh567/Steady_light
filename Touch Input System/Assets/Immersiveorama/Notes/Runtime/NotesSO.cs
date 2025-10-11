using System;
using System.Collections.Generic;
using UnityEngine;

namespace Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime
{
    public enum NoteCategory { General, Design, Code, Bug, ToDo }
    public enum NotePriority { Low, Medium, High, Critical }

    public class NotesSO : ScriptableObject
    {
        [Header("Basic Info")]
        public string title;
        [TextArea(3, 8)] public string description;

        [Header("Classification")]
        public List<string> tags = new List<string>();
        public NoteCategory category = NoteCategory.General;
        public NotePriority priority = NotePriority.Medium;

        [Header("References")]
        public UnityEngine.Object linkedAsset;            // Prefabs, scripts, materials etc.
        // public List<SceneObjectReference> linkedSceneObjects = new List<SceneObjectReference>();


        [Header("Metadata (Read-Only)")]
        [SerializeField] private string dateCreated;
        [SerializeField] private string dateModified;

        public string DateCreated => dateCreated;
        public string DateModified => dateModified;
    
        [Header("State")]
        public bool isPinned = false;
        public bool isClosed = false;

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(dateCreated))
            {
                dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                dateModified = dateCreated;
            }
        
        }

        public void MarkModified()
        {
            dateModified = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
    }
}