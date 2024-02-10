using System.Collections;
using UnityEngine;

public class PortalWalls : MonoBehaviour
{
    [SerializeField]
    private Transform _otherPortal;
    private PlayTeleportAudio _playTeleportAudio;
    public string _otherObjectName;
    public ParticleSystem _particleSystem;
    private void Start()
    {
        _playTeleportAudio = GetComponent<PlayTeleportAudio>();
        _particleSystem = GetComponent<ParticleSystem>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ball") || collision.CompareTag("Block") || collision.CompareTag("Star"))
        {
            StopCoroutine("TemporaryDisableOtherPortal");
            Rigidbody2D _otherObject = collision.GetComponent<Rigidbody2D>();
            if (_otherObjectName != collision.gameObject.name)
            {
                collision.transform.position = _otherPortal.position;
                if (SoundManager.SoundSfx)
                {
                    _playTeleportAudio.PlayAudio();
                }
                _otherPortal.GetComponent<PortalWalls>()._particleSystem.Play();
                _particleSystem.Play();
                float _objectMagnitude = _otherObject.velocity.magnitude;
                _otherObject.velocity = _otherPortal.transform.up * _objectMagnitude;
                _otherPortal.GetComponent<PortalWalls>()._otherObjectName = collision.gameObject.name;
            }
            StartCoroutine(TemporaryDisableOtherPortal());

        }
        else if (collision.CompareTag("Spike"))
        {
            StopCoroutine("TemporaryDisableOtherPortal");
            if (_otherObjectName != collision.gameObject.name)
            {
                if (collision.GetComponent<Rigidbody2D>() != null)
                {
                    float _objectMagnitude = collision.GetComponent<Rigidbody2D>().velocity.magnitude;
                    collision.GetComponent<Rigidbody2D>().velocity = _otherPortal.transform.up * _objectMagnitude;
                }
                collision.transform.position = _otherPortal.position;
                _otherPortal.GetComponent<PortalWalls>()._otherObjectName = collision.gameObject.name;
            }
            StartCoroutine(TemporaryDisableOtherPortal());

        }
    }

    IEnumerator TemporaryDisableOtherPortal()
    {
        StopCoroutine("TemporaryDisableOtherPortal");

        yield return new WaitForSeconds(3f);
        _otherPortal.GetComponent<PortalWalls>()._otherObjectName = "Free";

        StopCoroutine("TemporaryDisableOtherPortal");
    }
}
