using UnityEditor;
using UnityEngine;
using System.IO;

public static class DeleteSaveFile
{
    private static readonly string saveFilePath =
       Path.Combine(Application.persistentDataPath, "saveData1.sav");

    [MenuItem("AutoDo/Delete Save Data %#d")] 
    public static void DeleteFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log($"Deleted save file: {saveFilePath}");
        }
        else
        {
            Debug.LogWarning($"Save file not found: {saveFilePath}");
        }
    }
}
