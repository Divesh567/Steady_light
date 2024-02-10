using UnityEngine;
using UnityEngine.U2D;

public class FadeOutTrap : MonoBehaviour
{
    public void Triggered()
    {
        GetComponent<Collider2D>().enabled = !enabled;
        GetComponent<SpriteShapeRenderer>().color = new Color32(255, 255, 255, 0);
    }
}
