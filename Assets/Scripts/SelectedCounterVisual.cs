using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // The idea of this script is next:
    // 1. Player fire off an event whenever the selected counter changes
    // 2. All of the counter visuals listen to that event
    // 3. They identify if the event relates to that specific counter
    // 4. If so they update their state
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            visualGameObject.SetActive(true);
        }
        else
        {
            visualGameObject.SetActive(false);
        }
    }
}
