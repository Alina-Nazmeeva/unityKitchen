using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // this is a generic script to make the objects look straight at the camera
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private Mode mode;

    // runs after the Update()
    // LateUpdate() fits better here because we want canvas to turn after all actions inside the Update() is done
    // cutting counter is staic and simple Update() also fits, but for moving objects LateUpdate() fits better
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform); // turn the object to the camera
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward; // set the object's forward same as camera's
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
