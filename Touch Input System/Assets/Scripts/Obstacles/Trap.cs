using UnityEngine;
using UnityEngine.U2D;

public class Trap : MonoBehaviour
{
    public bool _FadeInTrap;
    public bool _FadeOutTrap;
    public bool _SpikeTrap;
    public bool _ObjectMoveTrap;
    public bool _DropTrap;

    public void Triggered()
    {
        if (_FadeInTrap)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if (_FadeOutTrap)
        {
            GetComponent<Collider2D>().enabled = !enabled;
            GetComponent<SpriteShapeRenderer>().color = new Color32(255, 255, 255, 0);
        }
        else if (_SpikeTrap)
        {
            gameObject.SetActive(true);
            if (GetComponent<Revolve>() != null)
            {
                GetComponent<Revolve>()._triggered = true;
            }
        }
        else if (_ObjectMoveTrap)
        {
            gameObject.GetComponent<MoveToPositon>()._move = true;
        }
        else if (_DropTrap)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
