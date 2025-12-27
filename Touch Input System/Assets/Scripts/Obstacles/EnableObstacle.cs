using UnityEngine;

public class EnableObstacle : Trap
{
    public SpriteRenderer SpriteRenderer;
    public Obstacle Obstacle;
    
    public override void Triggered()
    {
        SpriteRenderer.enabled = true;
        Obstacle.enabled = true;
    }

    public override void SetTrap()
    {
        SpriteRenderer.enabled = true;
        Obstacle.enabled = true;
        _trigger._traps.Add(this);
    }
}