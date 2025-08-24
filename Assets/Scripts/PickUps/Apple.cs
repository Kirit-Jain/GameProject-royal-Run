using UnityEngine;

public class Apple : PickUp
{
    protected override void OnPickUp()
    {
        Debug.Log("PowerUp");
    }
}
