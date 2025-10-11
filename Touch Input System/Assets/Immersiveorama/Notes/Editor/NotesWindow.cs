using System;
using System.Collections.Generic;
using System.Linq;
using Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Immersiveorama.EditorTools.Immersiveorama.Notes.Editor
{
    public class NotesWindow : EditorWindow
    {
        private NotesDatabase database;
        private NotesSO _selectedNote;
        private Vector2 scrollPos;

        // --- Filters ---
        private NoteCategory? filterCategory = null;
        private NotePriority? filterPriority = null;
        private enum SortMode { None, LastUpdated, Created }
        private SortMode sortMode = SortMode.None;
    
    

        [MenuItem("Tools/Project Notes #t")] // Shift+T
        public static void ShowWindow()
        {
            GetWindow<NotesWindow>("Project Notes");
        }

        private void OnEnable()
        {
            LoadDatabase();
        }

        private void LoadDatabase()
        {
            database = AssetDatabase.LoadAssetAtPath<NotesDatabase>("Assets/Notes/NoteDatabase.asset");

            if (database == null)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Notes"))
                    AssetDatabase.CreateFolder("Assets", "Notes");

                database = ScriptableObject.CreateInstance<NotesDatabase>();
                AssetDatabase.CreateAsset(database, "Assets/Notes/NoteDatabase.asset");
                AssetDatabase.SaveAssets();
            }
        }

        private void OnGUI()
        {
            HandleHotkeys(); // 🔥 process keys first

            DrawSearchBar();
            DrawFilters();

            EditorGUILayout.BeginHorizontal();
            DrawNotesList();
            DrawNoteDetails();
            EditorGUILayout.EndHorizontal();

            DrawFooter();


        }
        
        private string searchQuery = "";

        private void DrawSearchBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("🔍", GUILayout.Width(20));
            searchQuery = GUILayout.TextField(searchQuery, EditorStyles.toolbarTextField, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("X", EditorStyles.toolbarButton, GUILayout.Width(100)))
                searchQuery = "";
            EditorGUILayout.EndHorizontal();
        }
    
        [MenuItem("Window/Notes/Toggle Pin #p")]
        private static void TogglePin()
        {
            var window = GetWindow<NotesWindow>();
            if (window._selectedNote != null)
            {
                window._selectedNote.isPinned = !window._selectedNote.isPinned;
                window.SaveChanges();
                window.Repaint();
            }
        }

        [MenuItem("Window/Notes/Toggle Hide #h")]
        private static void ToggleHide()
        {
            var window = GetWindow<NotesWindow>();
            if (window._selectedNote != null)
            {
                window._selectedNote.isClosed = !window._selectedNote.isClosed;
                window.SaveChanges();
                window.Repaint();
            }
        }
    
        private void DrawFooter()
        {

            GUILayout.Label("Shortcuts:  Crtl + N - New Note | Del - Delete |  Shift + P - Pin | Shift + H - Hide | Arrows - Navigate", EditorStyles.miniLabel);
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        
            EditorGUILayout.EndHorizontal();
        }

        // === FILTER UI ===
        private bool showOnlyPinned = false;
        private bool showClosedNotes = false;

        private void DrawFilters()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            // existing filters ...
            // Category Filter
            string[] categories = Enum.GetNames(typeof(NoteCategory));
            int catIndex = filterCategory.HasValue ? (int)filterCategory.Value + 1 : 0;
            catIndex = EditorGUILayout.Popup(catIndex, new[] { "All Categories" }.Concat(categories).ToArray(), GUILayout.Width(150));
            filterCategory = (catIndex == 0) ? (NoteCategory?)null : (NoteCategory)(catIndex - 1);

            // Priority Filter
            string[] priorities = Enum.GetNames(typeof(NotePriority));
            int prioIndex = filterPriority.HasValue ? (int)filterPriority.Value + 1 : 0;
            prioIndex = EditorGUILayout.Popup(prioIndex, new[] { "All Priorities" }.Concat(priorities).ToArray(), GUILayout.Width(150));
            filterPriority = (prioIndex == 0) ? (NotePriority?)null : (NotePriority)(prioIndex - 1);

            sortMode = (SortMode)EditorGUILayout.EnumPopup(sortMode, GUILayout.Width(120));

            GUILayout.FlexibleSpace();

            // toggles
            showOnlyPinned = GUILayout.Toggle(showOnlyPinned, "📌 Pinned", EditorStyles.toolbarButton, GUILayout.Width(70));
            showClosedNotes = GUILayout.Toggle(showClosedNotes, "Closed", EditorStyles.toolbarButton, GUILayout.Width(70));

            if (GUILayout.Button("Clear Filters", EditorStyles.toolbarButton, GUILayout.Width(100)))
            {
                filterCategory = null;
                filterPriority = null;
                sortMode = SortMode.None;
                showOnlyPinned = false;
                showClosedNotes = false;
            }

            EditorGUILayout.EndHorizontal();
        }

        // === LEFT PANEL (List of Notes) ===
        private void DrawNotesList()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(200));
            GUILayout.Label("All Notes", EditorStyles.boldLabel);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            var filteredNotes = GetFilteredNotes();

            if (filteredNotes.Count() == 0)
            {
                GUILayout.Space(10);
                EditorGUILayout.HelpBox("No notes yet — click 'Create Note' to get started!", MessageType.Info);
            }
        
            foreach (var note in filteredNotes)
            {
                if (note == null) continue;

                string displayTitle = note.title;
                if (note.isPinned) displayTitle = "📌 " + displayTitle;
                if (note.isClosed) displayTitle = "❌ " + displayTitle;

                if (GUILayout.Button(displayTitle, (_selectedNote == note) ? EditorStyles.toolbarButton : EditorStyles.miniButton))
                {
                    _selectedNote = note;
                    GUI.FocusControl(null);
                }
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("+ Create Note"))
            {
                CreateNote();
            }

            if (_selectedNote != null && GUILayout.Button("Delete Selected"))
            {
                DeleteNote(_selectedNote);
            }

            EditorGUILayout.EndVertical();
        }

        // === RIGHT PANEL (Note Details) ===
        private void DrawNoteDetails()
        {
            EditorGUILayout.BeginVertical();

            if (_selectedNote == null)
            {
                GUILayout.Label("Select a note to view/edit", EditorStyles.helpBox);
                EditorGUILayout.EndVertical();
                return; // ✅ stop drawing early if nothing is selected
            }

            // --- Note editing UI ---
            EditorGUI.BeginChangeCheck();

            _selectedNote.title = EditorGUILayout.TextField("Title", _selectedNote.title);

            GUILayout.Label("Description");
            _selectedNote.description = EditorGUILayout.TextArea(_selectedNote.description, GUILayout.Height(60));

            _selectedNote.category = (NoteCategory)EditorGUILayout.EnumPopup("Category", _selectedNote.category);
            _selectedNote.priority = (NotePriority)EditorGUILayout.EnumPopup("Priority", _selectedNote.priority);

            GUILayout.Label("Tags (comma separated)");
            string tags = string.Join(", ", _selectedNote.tags);
            string newTags = EditorGUILayout.TextField(tags);
            if (newTags != tags)
                _selectedNote.tags = new List<string>(newTags.Split(','));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Created", _selectedNote.DateCreated);
            EditorGUILayout.LabelField("Modified", _selectedNote.DateModified);

            if (EditorGUI.EndChangeCheck())
            {
                _selectedNote.MarkModified();
                EditorUtility.SetDirty(_selectedNote);
                AssetDatabase.SaveAssets();
            }

            // --- Actions (Pin / Close) ---
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(_selectedNote.isPinned ? "📌 Unpin" : "📍 Pin", GUILayout.Width(70)))
            {
                _selectedNote.isPinned = !_selectedNote.isPinned;
                EditorUtility.SetDirty(_selectedNote);
            }

            if (GUILayout.Button(_selectedNote.isClosed ? "Reopen" : "Close", GUILayout.Width(70)))
            {
                _selectedNote.isClosed = !_selectedNote.isClosed;
                EditorUtility.SetDirty(_selectedNote);
            }

            var rect = GUILayoutUtility.GetRect(new GUIContent(titleContent), EditorStyles.miniButton);
            EditorGUI.DrawRect(new Rect(rect.x + 2, rect.y + 2, 8, rect.height - 4), GetTagColor(_selectedNote.category));

            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }


        // === Hotkeys (work only inside this window) ===
        private void HandleHotkeys()
        {
            Event e = Event.current;
            if (e.type != EventType.KeyDown) return;

            var notes = GetFilteredNotes().ToList();
            int currentIndex = (notes.Count > 0 && _selectedNote != null) ? notes.IndexOf(_selectedNote) : -1;

            switch (e.keyCode)
            {
                case KeyCode.N when e.control || e.command: // Ctrl/Cmd+N
                    CreateNote();
                    e.Use();
                    break;

                case KeyCode.Delete:
                    if (_selectedNote != null)
                    {
                        DeleteNote(_selectedNote);
                        e.Use();
                    }
                    break;

                case KeyCode.UpArrow:
                    if (notes.Count > 0 && currentIndex > 0)
                    {
                        _selectedNote = notes[currentIndex - 1];
                        Repaint();
                        e.Use();
                    }
                    break;

                case KeyCode.DownArrow:
                    if (notes.Count > 0 && currentIndex < notes.Count - 1)
                    {
                        _selectedNote = notes[currentIndex + 1];
                        Repaint();
                        e.Use();
                    }
                    break;
            }
        }

        // === Helpers ===
        private IEnumerable<NotesSO> GetFilteredNotes()
        {
            var filteredNotes = database.notes.Where(n => n != null);
        
            // Closed notes handling
            if (!showClosedNotes)
                filteredNotes = filteredNotes.Where(n => !n.isClosed);
        
            // Pinned notes filter
            if (showOnlyPinned)
                filteredNotes = filteredNotes.Where(n => n.isPinned);

            // Existing category & priority filters...
            if (filterCategory.HasValue)
                filteredNotes = filteredNotes.Where(n => n.category == filterCategory.Value);

            if (filterPriority.HasValue)
                filteredNotes = filteredNotes.Where(n => n.priority == filterPriority.Value);

            // Closed notes handling
            if (showClosedNotes)
                filteredNotes = filteredNotes.Where(n => n.isClosed);
        
            // Sorting
            switch (sortMode)
            {
                case SortMode.LastUpdated:
                    filteredNotes = filteredNotes.OrderByDescending(n => n.DateModified);
                    break;
                case SortMode.Created:
                    filteredNotes = filteredNotes.OrderByDescending(n => n.DateCreated);
                    break;
            }

            // Sort pinned notes to top always
            filteredNotes = filteredNotes.OrderByDescending(n => n.isPinned);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                filteredNotes = filteredNotes.Where(n =>
                    n.title.IndexOf(searchQuery, System.StringComparison.OrdinalIgnoreCase) >= 0);
            }
            
            return filteredNotes;

        }

        private void CreateNote()
        {
            NotesSO newNote = ScriptableObject.CreateInstance<NotesSO>();
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Notes/Note.asset");
            AssetDatabase.CreateAsset(newNote, path);
            AssetDatabase.SaveAssets();

            database.notes.Add(newNote);
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();

            _selectedNote = newNote;
        }

        private void DeleteNote(NotesSO note)
        {
            if (EditorUtility.DisplayDialog("Delete Note", "Are you sure you want to delete this note?", "Yes", "No"))
            {
                database.notes.Remove(note);
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(note));
                EditorUtility.SetDirty(database);
                AssetDatabase.SaveAssets();
                _selectedNote = null; // ✅ avoids null reference after deletion
            }
        }
        
        private Color GetTagColor(NoteCategory tag)
        {
            return tag switch
            {
                NoteCategory.Bug => new Color(0.9f, 0.3f, 0.3f),
                NoteCategory.ToDo => new Color(0.3f, 0.7f, 0.9f),
                NoteCategory.Design => new Color(0.8f, 0.8f, 0.3f),
                _ => Color.gray
            };
        }
    
    
    }
}
