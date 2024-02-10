using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private ForceFieldController _forceFieldController;
    private TouchController _playerTouchController;
    [SerializeField]
    private AudioClip _forceFieldDisableSfx;
    [SerializeField]
    private AudioClip _forceFieldEnableSfx;
    [SerializeField]
    private float _forceFieldDisableSfxVolume;
    [SerializeField]
    private float _forceFieldEnableeSfxVolume;


    private void Start()
    {
        _playerTouchController = transform.parent.GetComponent<TouchController>();
        _forceFieldController = GetComponent<ForceFieldController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().GetType() == typeof(CircleCollider2D) || GetComponent<Collider2D>().GetType() == typeof(CapsuleCollider2D))
        {
            if (collision.CompareTag("DeadZone"))
            {
                _forceFieldController.DisableForceField();
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlayForceFieldDisabled(_forceFieldDisableSfx, _forceFieldDisableSfxVolume);
                }
            }
        }

        else if (collision.gameObject.tag == "Ball")
        {
            _playerTouchController._force = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            _forceFieldController.EnableForceField();
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayForceFieldEnabled(_forceFieldEnableSfx, _forceFieldEnableeSfxVolume);
            }
        }
        if (collision.gameObject.tag == "Ball")
        {
            _playerTouchController._force = false;
        }
    }
}
