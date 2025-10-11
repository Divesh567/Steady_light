using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System;
using System.IO;
using System.Linq;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader _instance;
    private int _mainMenuIndex = 1;


    public AsyncOperationHandle<SceneInstance> handle;
    public static LevelLoader Instance { get { return _instance; } }

    public LevelHolder levelHolder;

    public AssetReference currentLevelAssetRef;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.LoadScene(1);
        levelHolder.worldSO.ForEach(x => x.InitializeLevelNames());
    }

    public void LoadNextLevel()
    {
        AssetReference nextLevelRef =  levelHolder.FindNextLevel(currentLevelAssetRef);

        UnloadLevel();

        // Debug.Log($"LOADING NEXT LEVEL{Path.GetFileName(nextLevelRef.editorAsset.name)}");

        LoadLevel(nextLevelRef);
    }

    public void LoadPreviousLevel()
    {
        AssetReference nextLevelRef = levelHolder.FindPreviousLevel(currentLevelAssetRef);

        UnloadLevel();

        // Debug.Log($"LOADING NEXT LEVEL{Path.GetFileName(nextLevelRef.editorAsset.name)}");

        LoadLevel(nextLevelRef);
    }


    public void LoadLevel(AssetReference levelName)
    {

        currentLevelAssetRef = levelName;


        RuntimeGameData.levelSelected = levelName;
        RuntimeGameData.levelType = levelHolder.FindLevelType(levelName);

        // Start loading the scene asynchronously
        handle = levelName.LoadSceneAsync();

        // Register a callback to be invoked when the load operation is complete
        handle.Completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        WinScreen.Instance.MenuClose();
        

        // Check the status of the operation
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            RuntimeGameData.levelSelectedName = handle.Result.Scene.name;
        }
        else if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError("Failed to load the scene.");
        }

        RuntimeGameData.worldType = levelHolder.FindWorldType(currentLevelAssetRef);

        GameMenu.Instance.MenuOpen();

        DataManager.Instance.SaveUnlockedLevel(RuntimeGameData.levelSelectedName, new List<int>());

        SceneTransitionManager.Instance.OnSceneTransitionCompleted.Invoke();

    }


    public void UnloadLevel()
    {
        if (handle.IsValid())
        {
            // Unload the scene
            Addressables.UnloadSceneAsync(handle, true).WaitForCompletion();

            // Release the handle to clean up resources
            Addressables.Release(handle);
        }
    }

    public void LoadMainMenu()
    {
        UnloadLevel();

        SceneManager.LoadSceneAsync(1);
    }

    public void ReloadLevel()
    {
       
        UnloadLevel();

        if (MenuManager.Instance != null && GameMenu.Instance != null)
        {
            MenuManager.Instance.CloseMenu(LoseScreen.Instance);
            MenuManager.Instance.OpenMenu(GameMenu.Instance);
        }

        LoadLevel(currentLevelAssetRef);

    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestScene");

        MenuManager.Instance.CloseMenu(MainMenu.Instance);
        MenuManager.Instance.OpenMenu(GameMenu.Instance);

        MyGameManager.Instance.StateChanged(MyGameManager.GameState.GameRunning);

    }


    [Serializable]
    public class LevelHolder
    {
        public List<WorldSO>  worldSO;

        public AssetReference FindNextLevel(AssetReference currentLevel)
        {
            List<WorldSO.LevelClass> allLevels = new List<WorldSO.LevelClass>();

            worldSO.ForEach(x =>
            {
                allLevels.AddRange(x.levels);
            });

            int currentIndex = allLevels.FindIndex(x => x.sceneAddress == currentLevel);


            return allLevels[currentIndex + 1].sceneAddress;
        }

        public AssetReference FindPreviousLevel (AssetReference currentLevel)
        {
            List<WorldSO.LevelClass> allLevels = new List<WorldSO.LevelClass>();


            worldSO.ForEach(x =>
            {
                allLevels.AddRange(x.levels);

            });

            int currentIndex = allLevels.FindIndex(x => x.sceneAddress == currentLevel);


            if(currentIndex - 1 < 0) 
                return  allLevels[0].sceneAddress;
            else
            return allLevels[currentIndex -1].sceneAddress;
        }

        public AssetReference FindCurrentLevel(List<SaveData.WorldData> data)
        {
            // Count total completed levels
            int totalCompletedLevels = 0;

            foreach (var worldData in data)
            {
                foreach (var level in worldData.levelsList)
                {
                    if (level.completed)
                        totalCompletedLevels++;
                    else
                        break;
                }
            }

            // Go through all worlds and subtract levels until we find the current one
            foreach (var world in worldSO)
            {
                if (totalCompletedLevels < world.levels.Count)
                {
                    // Found the current world
                    return world.levels[totalCompletedLevels].sceneAddress;
                }
                else
                {
                    // Skip completed world
                    totalCompletedLevels -= world.levels.Count;
                }
            }

            Debug.LogError("No current level found. All levels completed?");
            return null;
        }


        public WorldSO.LevelType FindLevelType(AssetReference currentLevel)
        {
            List<WorldSO.LevelClass> allLevels = new List<WorldSO.LevelClass>();


            worldSO.ForEach(x =>
            {
                allLevels.AddRange(x.levels);
            });

            var currentIndex = allLevels.Find(x => x.sceneAddress == currentLevel);

            return currentIndex.levelType;
        }

        public WorldSO.WorldType FindWorldType(AssetReference currentLevel)
        {
            // Find the world that contains the level with the given scene address
            var world = worldSO.Find(x => x.levels.Any(l => l.sceneAddress == currentLevel));

            if (world != null)
            {
                return world.worldType; // Return the world type if found
            }

            // Return a default value or throw an exception if not found
            Debug.LogError("World type not found for the specified level.");
            return WorldSO.WorldType.Basics; // Assuming Unknown is a valid default WorldType
        }

        public AssetReference GetLevelByNumber(int number)
        {
            int currentCount = 0;

            foreach (var world in worldSO)
            {
                foreach (var level in world.levels)
                {
                    if (currentCount == number)
                    {
                        return level.sceneAddress;
                    }
                    currentCount++;
                }
            }

            Debug.LogError($"No level found for number {number}. Total levels: {currentCount}");
            return null;


        }
    }

  

}
