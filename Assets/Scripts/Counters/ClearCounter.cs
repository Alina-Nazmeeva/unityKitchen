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
            else
            {
                // if player has a plate we can put KitchenObject on it
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // if this ingredient can be added to the plate, clear the counter
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // player has an ingredient NOT a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) // we can reuse same var
                    {
                        // counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            // if this ingredient can be added to the plate, clear the player
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}
