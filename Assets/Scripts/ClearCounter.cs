using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    // [SerializeField] private KitchenObjectSO kitchenObjectSO; // scriptable object
    // [SerializeField] private Transform counterTopPoint;
    // private KitchenObject kitchenObject; // ClearCounter knows if there's something on top of it

    // override the base Interact function
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
