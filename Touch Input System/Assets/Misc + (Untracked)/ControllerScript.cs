using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private GameObject _joystickBg;
    private GameObject _joystick;
    private Vector2 _joystickOriginalPos;
    private Vector2 _joystickTouchPos;
    private Vector2 _joystickVec;
    private float _joystickRadius;

    private void Start()
    {
        _joystickBg = transform.GetChild(0).gameObject;
        _joystick = transform.GetChild(1).gameObject;
        _joystickOriginalPos = _joystickBg.transform.position;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _joystickBg.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }





}
