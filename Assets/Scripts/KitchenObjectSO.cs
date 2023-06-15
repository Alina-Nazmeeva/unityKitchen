using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()] // attribute
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab; // public because it read only data container, however can be [Serialized] private
    public Sprite sprite; // icon
    public string objectName;
}
