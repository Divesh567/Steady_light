using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public bool _infinitePower = false;
    [SerializeField]
    private GameObject _fireHitVfx;
    [SerializeField]
    private GameObject _spikeDestroyVfx;
    public FireballPowerup _powerUpController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPowerUp(1);
            }
            Instantiate(_fireHitVfx, transform.position, transform.rotation);
            GameObject _spike = Instantiate(_spikeDestroyVfx, collision.transform.position, transform.rotation);
            _spike.transform.localScale = collision.transform.localScale;
            Destroy(collision.gameObject);
            StartCoroutine("FireBallHit");
        }
    }

    IEnumerator FireBallHit()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent.GetComponent<TrailRenderer>().enabled = true;
        transform.parent.GetComponent<BallCollisions>()._invulnerable = false;
        StopCoroutine("FireBallHit");
        _powerUpController._isPowerUpActive = false;
        Destroy(gameObject);
    }
}
