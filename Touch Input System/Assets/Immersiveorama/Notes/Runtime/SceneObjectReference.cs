using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime
{
    [System.Serializable]
    public class SceneObjectReference
    {
        [SerializeField] private string scenePath;       // Only for scene objects
        [SerializeField] private string hierarchyPath;   // Path in hierarchy for scene objects
        [SerializeField] private string assetGuid;       // Only for assets/prefabs
        [SerializeField] private long localId;           // Only for assets/prefabs

        public string ScenePath => scenePath;
        public string HierarchyPath => hierarchyPath;
        public string AssetGuid => assetGuid;
        public long LocalId => localId;

#if UNITY_EDITOR
        public void SetReference(Object obj)
        {
            if (obj == null) return;

            if (EditorUtility.IsPersistent(obj))
            {
                // Asset (prefab, material, etc.)
                string path = AssetDatabase.GetAssetPath(obj);
                assetGuid = AssetDatabase.AssetPathToGUID(path);
                localId = (long)Unsupported.GetLocalIdentifierInFileForPersistentObject(obj);
                scenePath = null;
                hierarchyPath = null;
            }
            else if (obj is GameObject go)
            {
                // Scene object
                scenePath = go.scene.path;
                hierarchyPath = GetHierarchyPath(go);
                assetGuid = null;
                localId = 0;
            }
        }

        public Object Resolve()
        {
            // Asset case
            if (!string.IsNullOrEmpty(assetGuid))
            {
                string path = AssetDatabase.GUIDToAssetPath(assetGuid);
                if (string.IsNullOrEmpty(path)) return null;
                return AssetDatabase.LoadAssetAtPath<Object>(path);
            }

            // Scene object case
            if (!string.IsNullOrEmpty(scenePath) && !string.IsNullOrEmpty(hierarchyPath))
            {
                var scene = SceneManager.GetSceneByPath(scenePath);
                if (!scene.isLoaded)
                {
                    scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                }

                foreach (var root in scene.GetRootGameObjects())
                {
                    var found = FindByHierarchyPath(root, hierarchyPath);
                    if (found != null) return found;
                }
            }

            return null;
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
#endif
    }
}
