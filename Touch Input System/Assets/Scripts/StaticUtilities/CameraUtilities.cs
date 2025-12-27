using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtilities 
{
     public static Vector2 GetScreenPos(Transform transform , Canvas canvas)
     {


        var canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        return WorldObject_ScreenPosition;

     }
     
     public static  void SetTransfromPosition(Transform transform)
     {
         if (Camera.main != null)
         {
             Vector3 centerPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
             transform.position =  centerPos;
         }
         else
         {
             Debug.LogError("Camera main object not found");
         }
        
     }
}
