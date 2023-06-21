using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;

    // [SerializeField] private IHasProgress hasProgress; // this field is not exposed in the editor, because it is interface
    // this is a workaround to expose it in the editor:
    // 1. expose GameObject field
    [SerializeField] private GameObject hasProgressGameObject;
    // 2. make interface simple private field
    private IHasProgress hasProgress;

    private void Start()
    {
        // 3. assign hasProgress with IHasProgress component of Game Object
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        // 4. check that hasProgressGameObject has IHasProgress component
        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;
        Hide(); // it is important to hide only after the subscribtion to the event
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f) Hide();
        else Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
