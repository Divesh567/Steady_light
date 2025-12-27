using System;
using System.Collections.Generic;
using System.Linq;
using Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            database = AssetDatabase.LoadAssetAtPath<NotesDatabase>("Assets/Notes/NotesDatabase.asset");

            if (database == null)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Notes"))
                    AssetDatabase.CreateFolder("Assets", "Notes");

                database = ScriptableObject.CreateInstance<NotesDatabase>();
                AssetDatabase.CreateAsset(database, "Assets/Notes/NotesDatabase.asset");
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

            GUILayout.Label("Shortcuts:  Crtl + N - New Note | Del - Delete |  Shift + P - Pin | Shift + H - Close | Arrows - Navigate", EditorStyles.miniLabel);
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        
            EditorGUILayout.EndHorizontal();
        }

        // === FILTER UI ===
        private bool showOnlyPinned = false;
        private bool showClosedNotes = false;
        private bool focusTitleFieldNextFrame;

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

           GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
           {
               alignment = TextAnchor.MiddleLeft, padding = new RectOffset(10, 10, 4, 4)
           };

           foreach (var note in filteredNotes)
           {
               if (note == null) continue;

               string displayTitle = note.title;
               if (note.isPinned) displayTitle = "📌 " + displayTitle;
               if (note.isClosed) displayTitle = "❌ " + displayTitle;

               // ✂️ Limit title length to 15 characters max
               const int maxLength = 15;
               if (displayTitle.Length > maxLength)
                   displayTitle = displayTitle.Substring(0, maxLength - 3) + "...";

               // Choose style for selected note
               GUIStyle activeStyle = (_selectedNote == note)
                   ? new GUIStyle(EditorStyles.toolbarButton) { alignment = TextAnchor.MiddleLeft }
                   : buttonStyle;

               // Reserve rect for button
               Rect rect = GUILayoutUtility.GetRect(new GUIContent(displayTitle), activeStyle, GUILayout.Height(25));

               // Draw button and handle click
               if (GUI.Button(rect, displayTitle, activeStyle))
               {
                   _selectedNote = note;
                   GUI.FocusControl(null);
               }

               // Draw category color box (right aligned)
               Color categoryColor = GetCategoryColor(note.category);
               float boxSize = 10f;
               Rect colorRect = new Rect(rect.xMax - boxSize - 6, rect.y + (rect.height - boxSize) / 2f, boxSize, boxSize);
               EditorGUI.DrawRect(colorRect, categoryColor);

               // Outline the color box
               EditorGUI.DrawRect(new Rect(colorRect.x - 1, colorRect.y - 1, colorRect.width + 2, 1), Color.black); // top
               EditorGUI.DrawRect(new Rect(colorRect.x - 1, colorRect.yMax, colorRect.width + 2, 1), Color.black); // bottom
               EditorGUI.DrawRect(new Rect(colorRect.x - 1, colorRect.y - 1, 1, colorRect.height + 2), Color.black); // left
               EditorGUI.DrawRect(new Rect(colorRect.xMax, colorRect.y - 1, 1, colorRect.height + 2), Color.black); // right

               // Highlight selected note
               if (_selectedNote == note)
                   EditorGUI.DrawRect(rect, new Color(0.3f, 0.6f, 1f, 0.1f));
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
        private Vector2 detailScroll;

private void DrawNoteDetails()
{
    if (_selectedNote == null)
    {
        GUILayout.Label("Select a note to view/edit", EditorStyles.helpBox);
        return;
    }

    detailScroll = EditorGUILayout.BeginScrollView(detailScroll);
    EditorGUILayout.BeginVertical("box");
    GUILayout.Space(4);

    EditorGUI.BeginChangeCheck();

    // --- Title ---
    GUILayout.Label("Title", EditorStyles.boldLabel);
    _selectedNote.title = EditorGUILayout.TextField(_selectedNote.title);

    // --- Description ---
    GUILayout.Space(4);
    GUILayout.Label("Description", EditorStyles.boldLabel);
    _selectedNote.description = EditorGUILayout.TextArea(_selectedNote.description, GUILayout.MinHeight(60));

    // --- Category & Priority ---
    GUILayout.Space(4);
    _selectedNote.category = (NoteCategory)EditorGUILayout.EnumPopup("Category", _selectedNote.category);
    _selectedNote.priority = (NotePriority)EditorGUILayout.EnumPopup("Priority", _selectedNote.priority);

    /*
    // --- Tags ---
    GUILayout.Space(4);
    GUILayout.Label("Tags (comma separated)", EditorStyles.boldLabel);
    string tags = string.Join(", ", _selectedNote.tags);
    string newTags = EditorGUILayout.TextField(tags);
    if (newTags != tags)
        _selectedNote.tags = new List<string>(newTags.Split(','));
        */

    // --- Dates ---
    EditorGUILayout.Space();
    using (new EditorGUILayout.HorizontalScope())
    {
        EditorGUILayout.LabelField("Created", _selectedNote.DateCreated, EditorStyles.miniLabel);
        EditorGUILayout.LabelField("Modified", _selectedNote.DateModified, EditorStyles.miniLabel);
    }

    // --- Preview Image (small & right-aligned) ---
    GUILayout.Space(6);

    using (new EditorGUILayout.HorizontalScope())
    {
        GUILayout.Label("Preview Image", EditorStyles.boldLabel);

        GUILayout.FlexibleSpace(); // push preview to the right

        if (_selectedNote.previewImage != null)
        {
            Texture2D tex = _selectedNote.previewImage.texture;
            if (tex != null)
            {
                float previewSize = 80f; // smaller thumbnail
                float aspect = (float)tex.width / tex.height;
                float width = previewSize;
                float height = previewSize / aspect;

                Rect rect = GUILayoutUtility.GetRect(width, height, GUILayout.Width(width), GUILayout.Height(height));
                EditorGUI.DrawPreviewTexture(rect, tex, null, ScaleMode.ScaleToFit);

                // Optional: click to ping/select image in project
                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    EditorGUIUtility.PingObject(_selectedNote.previewImage);
                    Selection.activeObject = _selectedNote.previewImage;
                    Event.current.Use();
                }
            }
        }
    }

    _selectedNote.previewImage = (Sprite)EditorGUILayout.ObjectField(_selectedNote.previewImage, typeof(Sprite), false);


    // --- Attached Object Section ---
    GUILayout.Space(6);
    if (!string.IsNullOrEmpty(_selectedNote.attachedScenePath) || !string.IsNullOrEmpty(_selectedNote.attachedAssetPath))
    {
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label("Attachment", EditorStyles.boldLabel);

            EditorGUILayout.LabelField(
                "Path:",
                !string.IsNullOrEmpty(_selectedNote.attachedAssetPath)
                    ? _selectedNote.attachedAssetPath
                    : _selectedNote.attachedHierarchyPath,
                EditorStyles.wordWrappedMiniLabel
            );

            if (GUILayout.Button("Go To Attached Object", GUILayout.Height(24)))
            {
                bool ok = TrySelectObjectFromAttachment(
                    _selectedNote.attachedScenePath,
                    _selectedNote.attachedHierarchyPath,
                    _selectedNote.attachedAssetPath
                );
                if (!ok)
                    EditorUtility.DisplayDialog("Note", "Target object not found. It may have been renamed or moved.", "OK");
            }
        }
    }

    // --- Actions (Pin / Close) ---
    GUILayout.Space(8);
    using (new EditorGUILayout.HorizontalScope())
    {
        if (GUILayout.Button(_selectedNote.isPinned ? "📌 Unpin" : "📍 Pin", GUILayout.Width(80)))
        {
            _selectedNote.isPinned = !_selectedNote.isPinned;
            EditorUtility.SetDirty(_selectedNote);
        }

        if (GUILayout.Button(_selectedNote.isClosed ? "Reopen" : "Close", GUILayout.Width(80)))
        {
            _selectedNote.isClosed = !_selectedNote.isClosed;
            EditorUtility.SetDirty(_selectedNote);
        }
    }

    // --- Save modified ---
    if (EditorGUI.EndChangeCheck())
    {
        _selectedNote.MarkModified();
        EditorUtility.SetDirty(_selectedNote);
        AssetDatabase.SaveAssets();
    }

    EditorGUILayout.EndVertical();
    EditorGUILayout.EndScrollView();
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
                        SelectNote(notes[currentIndex - 1]);
                        e.Use();
                    }
                    break;

                case KeyCode.DownArrow:
                    if (notes.Count > 0 && currentIndex < notes.Count - 1)
                    {
                        SelectNote(notes[currentIndex + 1]);
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
            var newNote = ScriptableObject.CreateInstance<NotesSO>();
            newNote.title = "New Note";
            newNote.description = "";

            // Ensure note is treated as part of the database and hidden from hierarchy
            newNote.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;

            // Add to database
            database.notes.Add(newNote);
            AssetDatabase.AddObjectToAsset(newNote, database); // makes NotesDatabase the root container
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Select new note
            _selectedNote = newNote;
            focusTitleFieldNextFrame = true; // trigger focusing
        }


        private void DeleteNote(NotesSO note)
        {
            if (note == null)
                return;
            
            if (EditorUtility.DisplayDialog("Delete Note", "Are you sure you want to delete this note?", "Yes", "No"))
            {
                // Remove from database list
                database.notes.Remove(note);

                // Temporarily clear hideFlags so Unity allows deletion
                note.hideFlags = HideFlags.None;

                // Remove from database asset
                AssetDatabase.RemoveObjectFromAsset(note);

                // Destroy the ScriptableObject
                Object.DestroyImmediate(note, true);

                EditorUtility.SetDirty(database);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                _selectedNote = null;
            }
        }


        
        private Color GetCategoryColor(NoteCategory tag)
        {
            return tag switch
            {
                NoteCategory.Bug => new Color(0.9f, 0.3f, 0.3f),
                NoteCategory.ToDo => new Color(0.3f, 0.2f, 0.7f),
                NoteCategory.Design => new Color(0.8f, 0.8f, 0.3f),
                NoteCategory.Code => new Color(0.3f, 0.7f, 0.9f),

                _ => Color.gray
            };
        }
        
        public bool TrySelectObjectFromAttachment(string scenePath, string hierarchyPath, string assetPath)
        {
            // If it's an asset (prefab), just ping/select the asset
            if (!string.IsNullOrEmpty(assetPath))
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (asset != null)
                {
                    EditorGUIUtility.PingObject(asset);
                    Selection.activeObject = asset;
                    return true;
                }
                return false;
            }

            // Scene object path flow
            if (string.IsNullOrEmpty(scenePath) || string.IsNullOrEmpty(hierarchyPath))
                return false;

            // Ask user to save modified scenes first
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return false;

            // Open scene (single mode)
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            if (!scene.IsValid())
            {
                Debug.LogWarning($"Unable to open scene: {scenePath}");
                return false;
            }

            // find the object by hierarchy path
            GameObject found = FindGameObjectInSceneByHierarchyPath(scene, hierarchyPath);
            if (found != null)
            {
                Selection.activeGameObject = found;          // select
                EditorGUIUtility.PingObject(found);          // ping
                return true;
            }
            else
            {
                Debug.LogWarning($"Attached object not found in scene. Path: {hierarchyPath}");
                return false;
            }
        }

        private GameObject FindGameObjectInSceneByHierarchyPath(Scene scene, string hierarchyPath)
        {
            if (!scene.isLoaded) return null;
            string[] parts = hierarchyPath.Split('/');
            foreach (var root in scene.GetRootGameObjects())
            {
                if (root.name != parts[0]) continue;
                Transform t = root.transform;
                bool fail = false;
                for (int i = 1; i < parts.Length; ++i)
                {
                    t = t.Find(parts[i]);
                    if (t == null) { fail = true; break; }
                }
                if (!fail) return t.gameObject;
            }
            return null;
        }

        private SerializedObject serializedNote;
        private void SelectNote(NotesSO note)
        {
            if (note == _selectedNote) return;

            _selectedNote = note;

            // Force-refresh SerializedObject for the details panel
            if (_selectedNote != null)
            { 
                serializedNote = new SerializedObject(_selectedNote);
                var titleProp = serializedNote.FindProperty("title");
                var categoryProp = serializedNote.FindProperty("category");
                var descriptionProp = serializedNote.FindProperty("description");
                // Add other properties if your NotesSO has more
            }
            else
            {
                serializedNote = null;
            }

            GUI.FocusControl(null);
            Repaint();
        }


    }
}
