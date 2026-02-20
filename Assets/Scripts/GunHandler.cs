using UnityEngine;

public class GunHandler : InteractionHandler
{
    public int Magazine;
    public float FireRate;
    public string Name;

    private void Awake()
    {
        
    }

    public override string SetInteractionText()
    {
        return "Press E to pick up " + Name;
    }

    public override void Interact()
    {
        PickUp();
    }

    public void PickUp() 
    {

    }

    public void Drop() 
    { 

    }

    public void Shoot() 
    { 

    }

    public void Reload() 
    { 

    }
}
