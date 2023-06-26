using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    // event for adding ingredient visuals to the plate complete visual and to the PlateIconsUI
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    // public void AddIngredient(KitchenObjectSO kitchenObjectSO)
    // {
    //     kitchenObjectSOList.Add(kitchenObjectSO);
    // }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // check if it a valid ingredient
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        // there can be only one instance of each ingredient
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // already has this type
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
