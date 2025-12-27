
public class DisableObject : Trap
{
    
    public override void Triggered()
    {
        this.gameObject.SetActive(false);
    }

    public override void SetTrap()
    {
        this.gameObject.SetActive(true);
        _trigger._traps.Add(this);
    }
}