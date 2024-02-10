using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    private GameObject _forceField;
    private Animator _animator;
    private Collider2D _collider2D;

    private void Start()
    {
        _forceField = transform.GetChild(0).gameObject;
        _animator = GetComponent<Animator>();
        _collider2D = _forceField.GetComponent<Collider2D>();
    }
    public void DisableForceField()
    {
        _animator.SetBool("Enabled", false);
        _collider2D.enabled = false;
    }

    public void EnableForceField()
    {
        _animator.SetBool("Enabled", true);
        _collider2D.enabled = true;
    }
}
