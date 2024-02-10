using UnityEngine;


public class repositionself : MonoBehaviour
{
    [SerializeField]
    private string _repositioner;
    private Vector3 _startpos;
    private Vector3 parentStartPos;
    private void Start()
    {
        _startpos = transform.position;
        if (transform.parent != null)
        {
            parentStartPos = transform.parent.gameObject.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_repositioner))
        {
            transform.position = _startpos;
            if (transform.parent != null)
            {
                transform.parent.gameObject.transform.position = parentStartPos;
            }
        }
    }
}
