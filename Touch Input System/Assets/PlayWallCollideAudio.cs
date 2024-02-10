using UnityEngine;

public class PlayWallCollideAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _bouceVfx;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _audioSource.Play();
            if (transform.childCount > 0)
            {
                Instantiate(_bouceVfx, transform.GetChild(0).transform.position, _bouceVfx.transform.rotation);
            }
            else
            {

            }
        }
    }
}
