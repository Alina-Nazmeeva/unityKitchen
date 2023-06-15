using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject; // event for animation
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // scriptable object

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // generate kitchen object and give it to the player
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // fire off the event for animation
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
