#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomButtonMenu
{
    [MenuItem("IMR/UI/Custom Button")]
    static void CreateCustomButton(MenuCommand menuCommand)
    {
        // Create parent GameObject
        GameObject go = new GameObject("CustomButton", typeof(RectTransform));
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

        // Add components
        Image img = go.AddComponent<Image>();
        Button btn = go.AddComponent<Button>();
        CustomButton cb = go.AddComponent<CustomButton>();

        // Add TMP child
        GameObject textGO = new GameObject("Text", typeof(RectTransform));
        textGO.transform.SetParent(go.transform, false);
        TextMeshProUGUI tmp = textGO.AddComponent<TextMeshProUGUI>();
        tmp.text = "Button";
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.raycastTarget = false;

        // Anchor text
        RectTransform rect = textGO.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        // Assign references
        cb.button = btn;
        cb.image = img;
        cb.textMesh = tmp;

        // Select in hierarchy
        Selection.activeGameObject = go;
    }
}
#endif
