using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomToggleButton : CustomButton
{
    public enum ToggleType 
    {
        SfxToggle,
        MusicToggle,
    }

    private bool _value;
    public bool value { get { return _value; } set { _value = value; } }

    [Header("Toggle Params")]
    public ToggleType type;
    [SerializeField]
    private Sprite toggleOnSprite;
    [SerializeField]
    private Sprite toggleOffSprite;

    [SerializeField]
    private string toggleOnText;
    [SerializeField]
    private string toggleOffText;

    public override void Start()
    {
        base.Start();
        button.onClick.AddListener(() => OnValueChanged(!_value));

        SetInitValue();
    }

    private void SetInitValue()
    {
        if(type == ToggleType.SfxToggle)
        {
            value = !DataManager.Instance.isSfxMuted;
        }
        else
        {
            value = !DataManager.Instance.isMuiscMuted;
        }

        UpdateVisuals();
    }
    public void OnValueChanged(bool value)
    {
        _value = value;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_value == false)
        {
            image.sprite = toggleOffSprite;
            textMesh.text = toggleOffText;
        }
        else
        {
            image.sprite = toggleOnSprite;
            textMesh.text = toggleOnText;
        }
    }
}
