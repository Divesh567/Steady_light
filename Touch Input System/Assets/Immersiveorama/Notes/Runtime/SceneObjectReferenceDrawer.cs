using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime
{
    [CustomPropertyDrawer(typeof(SceneObjectReference))]
    public class SceneObjectReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var scenePathProp     = property.FindPropertyRelative("scenePath");
            var hierarchyPathProp = property.FindPropertyRelative("hierarchyPath");
            var assetGuidProp     = property.FindPropertyRelative("assetGuid");
            var localIdProp       = property.FindPropertyRelative("localId");

            // Layout: [ObjectField] [Preview Label] [Ping Button]
            float fieldWidth = position.width * 0.35f;
            float previewWidth = position.width * 0.35f;
            float buttonWidth = position.width * 0.25f;

            Rect fieldRect = new Rect(position.x, position.y, fieldWidth, position.height);
            Rect previewRect = new Rect(position.x + fieldWidth + 2, position.y, previewWidth - 4, position.height);
            Rect buttonRect = new Rect(position.x + fieldWidth + previewWidth, position.y, buttonWidth, position.height);

            // --- Drag & Drop Field ---
            Object dropped = EditorGUI.ObjectField(fieldRect, label, null, typeof(Object), true);

            if (dropped != null)
            {
                if (EditorUtility.IsPersistent(dropped))
                {
                    // Asset reference
                    string path = AssetDatabase.GetAssetPath(dropped);
                    assetGuidProp.stringValue = AssetDatabase.AssetPathToGUID(path);
                    localIdProp.longValue = (long)Unsupported.GetLocalIdentifierInFileForPersistentObject(dropped);
                    scenePathProp.stringValue = null;
                    hierarchyPathProp.stringValue = null;
                }
                else if (dropped is GameObject go)
                {
                    // Scene object reference
                    scenePathProp.stringValue = go.scene.path;
                    hierarchyPathProp.stringValue = GetHierarchyPath(go);
                    assetGuidProp.stringValue = null;
                    localIdProp.longValue = 0;
                }
            }

            // --- Preview Label ---
            string previewText = "(None)";
            if (!string.IsNullOrEmpty(assetGuidProp.stringValue))
            {
                string path = AssetDatabase.GUIDToAssetPath(assetGuidProp.stringValue);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                if (asset != null) previewText = asset.name;
            }
            else if (!string.IsNullOrEmpty(scenePathProp.stringValue) && !string.IsNullOrEmpty(hierarchyPathProp.stringValue))
            {
                previewText = $"{System.IO.Path.GetFileNameWithoutExtension(scenePathProp.stringValue)}:{hierarchyPathProp.stringValue}";
            }

            EditorGUI.LabelField(previewRect, previewText, EditorStyles.miniLabel);

            // --- Ping Button ---
            if (GUI.Button(buttonRect, "Ping"))
            {
                // Asset case
                if (!string.IsNullOrEmpty(assetGuidProp.stringValue))
                {
                    string path = AssetDatabase.GUIDToAssetPath(assetGuidProp.stringValue);
                    var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
                    if (obj != null)
                    {
                        Selection.activeObject = obj;
                        EditorGUIUtility.PingObject(obj);
                    }
                }
                // Scene object case
                else if (!string.IsNullOrEmpty(scenePathProp.stringValue) && !string.IsNullOrEmpty(hierarchyPathProp.stringValue))
                {
                    var scene = SceneManager.GetSceneByPath(scenePathProp.stringValue);
                    if (!scene.isLoaded)
                    {
                        scene = EditorSceneManager.OpenScene(scenePathProp.stringValue, OpenSceneMode.Additive);
                    }

                    foreach (var root in scene.GetRootGameObjects())
                    {
                        var obj = FindByHierarchyPath(root, hierarchyPathProp.stringValue);
                        if (obj != null)
                        {
                            Selection.activeObject = obj;
                            EditorGUIUtility.PingObject(obj);
                            break;
                        }
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        private string GetHierarchyPath(GameObject go)
        {
            string path = go.name;
            Transform current = go.transform.parent;
            while (current != null)
            {
                path = current.name + "/" + path;
                current = current.parent;
            }
            return path;
        }

        private GameObject FindByHierarchyPath(GameObject root, string path)
        {
            string rootPath = GetHierarchyPath(root);
            if (rootPath == path) return root;

            foreach (Transform child in root.transform)
            {
                var found = FindByHierarchyPath(child.gameObject, path);
                if (found != null) return found;
            }
            return null;
        }
    }
}
