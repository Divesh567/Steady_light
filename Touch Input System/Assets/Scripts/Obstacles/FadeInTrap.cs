using System.Collections;
using UnityEngine;

public class FadeInTrap : MonoBehaviour
{
    [SerializeField]
    private bool _modifier = false;
    public void Triggered()
    {

        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        if (_modifier)
        {
            StartCoroutine(TrapActiveCountDown());
        }
    }

    IEnumerator TrapActiveCountDown()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
    }
}
