using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class MenuMainPanel : MonoBehaviour
{
    [Header("Panel RectTransform")]
    [SerializeField]
    private RectTransform _panelRect;
    public RectTransform PanelRect { get => _panelRect; set => _panelRect = value; }


    [Header("Panel Mask Image")]
    [SerializeField]
    private Image _panelImage;
    public Image PanelImage { get => _panelImage; set => _panelImage = value; }



}
