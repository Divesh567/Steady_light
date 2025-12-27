using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public List<LineRenderer> lines;
    public float drawDuration = 1.5f;
    public LineRenderer currentLine;
    
    
    [ContextMenu("Draw")]
    private void DrawLineTest()
    {
        int count = currentLine.positionCount;
        Vector3[] points = new Vector3[count];
        currentLine.GetPositions(points);

        DrawSignature(points).Forget();
    }

    public async UniTask DrawSignature(Vector3[] points)
    {
        currentLine.positionCount = 0;

        float stepTime = drawDuration / points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            currentLine.positionCount = i + 1;
            currentLine.SetPosition(i, points[i]);
            await UniTask.Delay((int)(stepTime * 1000));
        }
    }

    public async UniTask Execute()
    {
        CameraUtilities.SetTransfromPosition(transform);
        
        int currentLevelIndex = 0;
        
        /*var world = LevelLoader.Instance.levelHolder.worldSO.Find(x => x.worldType == RuntimeGameData.worldType);
        currentLevelIndex = world.levels.FindIndex(x => x.sceneName == RuntimeGameData.levelSelectedName);*/

        LineRenderer currentLevelSignature = lines[currentLevelIndex];
        
        currentLine = Instantiate(currentLevelSignature,transform);
        
        await UniTask.DelayFrame(1);
        
        int count = currentLevelSignature.positionCount;
        Vector3[] points = new Vector3[count];
        currentLevelSignature.GetPositions(points);

        DrawSignature(points).Forget();

        MenuManager.Instance.OpenMenu(WinScreen.Instance);
        MenuManager.Instance.CloseMenu(GameMenu.Instance);
    }
}
